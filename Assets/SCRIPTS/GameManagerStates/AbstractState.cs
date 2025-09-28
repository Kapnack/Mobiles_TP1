using System;

namespace GameManagerStates
{
    public abstract class AbstractState<T> where T : class 
    {
        protected T entity;
        public abstract void Cambiar(T gameManager);
        public abstract void Update();
        public abstract void Empezar();
        
        public abstract void Finalizar();
        
    }
}