using Elasticsearch.Index;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;

        public PersonController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpGet("{id}")]
        public async Task<Person> Get(string id)
        {
            var response = await _elasticClient.SearchAsync<Person>(s => s
            .Index("persons")
            .Query(q => q.Match(m => m.Field(f => f.PrivateNumber).Query(id))));

            return response?.Documents?.FirstOrDefault();
        }
    }
}
