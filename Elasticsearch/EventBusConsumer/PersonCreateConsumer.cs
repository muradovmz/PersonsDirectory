using Elasticsearch.Index;
using EventBus.Messages.Events;
using MassTransit;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elasticsearch.EventBusConsumer
{
    public class PersonCreateConsumer : IConsumer<PersonCreateEvent>
    {
        private readonly IElasticClient _elasticClient;

        public PersonCreateConsumer(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<PersonCreateEvent> context)
        {
            //add to elasticsearch
            await _elasticClient.IndexAsync(context.Message, x => x.Index("persons"));
        }
    }
}
