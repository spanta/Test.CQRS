﻿using System;
using System.Linq;
using System.Collections.Generic;
using Test.CQRS.Infrastructure.Exceptions;
using Test.CQRS.Infrastructure.Storage.Memento;
using Test.CQRS.WriteSide.Common.Domain;
using Test.CQRS.WriteSide.Common.Events;

namespace Test.CQRS.Infrastructure.Storage
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStorage _storage;
        private static object _lockStorage = new object();

        public Repository(IEventStorage storage)
        {
            _storage = storage;
        } 

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            if (aggregate.GetUncommittedChanges().Any())
            {
                lock (_lockStorage)
                {
                    var item = new T();

                    if (expectedVersion != -1)
                    {
                        item = GetById(aggregate.Id);
                        if (item.Version != expectedVersion)
                        {
                            throw new ConcurrencyException(string.Format("Aggregate {0} has been previously modified",
                                                                         item.Id));
                        }
                    }

                    _storage.Save(aggregate);
                }
            }
        }

        public T GetById(Guid id)
        {
           IEnumerable<Event> events;
           var memento = _storage.GetMemento<BaseMemento>(id);
           if (memento != null)
           {
               events = _storage.GetEvents(id).Where(e=>e.Version>=memento.Version);
           }
           else
           {
               events = _storage.GetEvents(id);
           }
            var obj = new T();
            if(memento!=null)
                ((IOriginator)obj).SetMemento(memento);
            
            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}
