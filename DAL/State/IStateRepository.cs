using Sample_CRUD.Models;
using System;
using System.Collections.Generic;

namespace Sample_CRUD.DAL
{
    public interface IStateRepository : IDisposable
    {
        IEnumerable<State> GetStates();
        State GetStateByID(int StateId);        
        void InsertState(State state);        
        void DeleteState(int StateID);
        void UpdateState(State state);
        void Save();        
    }
}