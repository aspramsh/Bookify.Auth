using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using IdentityServer4.Postgresql.Mappers;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Marten;
using System.Linq;
using IdentityServer4.Postgresql.Entities;

namespace IdentityServer4.Postgresql.Stores
{
	public class ResourceStore : IResourceStore
	{
		private readonly IDocumentSession _documentSession;
		public ResourceStore(IDocumentSession documentSession)
		{
			_documentSession = documentSession;
		}
		public async Task<ApiResource> FindApiResourceAsync(string name)
		{
			var resource = await _documentSession.Query<PostgresApiResource>().FirstOrDefaultAsync(_ => _.Name == name).ConfigureAwait(false);
			return resource.ToModel();
		}

		public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

			var resources = _documentSession.Query<PostgresApiResource>().ToList();
			return Task.FromResult(resources.Where(x => scopeNames.Contains(x.Name)).Select(x => x.ToModel()).AsEnumerable());
		}

		public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
		{
			if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

			var identities = _documentSession.Query<PostgresIdentityResource>().ToList();
			return Task.FromResult(identities.Where(x => scopeNames.Contains(x.Name)).Select(x => x.ToModel()).AsEnumerable());
		}

		public Task<Resources> GetAllResourcesAsync()
		{
			var identityResources = _documentSession.Query<PostgresIdentityResource>().ToList();
			var apiResources = _documentSession.Query<PostgresApiResource>().ToList();
			return Task.FromResult(new Resources(identityResources.Select(x => x.ToModel()), apiResources.Select(x => x.ToModel())));
		}
	}
}
