using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Sample_CRUD.Models;
using System.Data.Entity;

namespace Sample_CRUD.DAL
{
    public class StateRepository : IStateRepository
    {
        private SampleCrudContext _context;

        public StateRepository(SampleCrudContext dbContext)
        {
            this._context = dbContext;
        }

        public IEnumerable<State> GetStates()
        {
            return _context.State.ToList();
        }

        public State GetStateByID(int id)
        {
            return _context.State.Find(id);
        }

        public void InsertState(State state)
        {
            _context.State.Add(state);
        }

        public void DeleteState(int stateID)
        {
            State state = _context.State.Find(stateID);
            _context.State.Remove(state);
        }

        public void UpdateState(State state)
        {
            _context.Entry(state).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
