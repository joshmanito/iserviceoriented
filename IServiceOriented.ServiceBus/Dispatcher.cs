﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;


namespace IServiceOriented.ServiceBus
{    
    /// <summary>
    /// Base class for all service bus dispatchers.
    /// </summary>
    /// <remarks>
    /// Dispatchers are responsible for delivering messages to subscription endpoints.
    /// </remarks>
    [Serializable]
    [DataContract]
    public abstract class Dispatcher : IDisposable
    {
        protected Dispatcher()
        {
        }
        protected Dispatcher(SubscriptionEndpoint endpoint)
        {
            _endpoint = endpoint;
        }
        [NonSerialized]
        ServiceBusRuntime _runtime;
        /// <summary>
        /// Gets the service bus instance associated with this dispatcher.
        /// </summary>
        public ServiceBusRuntime Runtime
        {
            get
            {
                return _runtime;
            }
            internal set
            {
                _runtime = value;
            }
        }

        [NonSerialized]
        SubscriptionEndpoint _endpoint;        
        /// <summary>
        /// Gets the subscription endpoint associated with this dispatcher.
        /// </summary>
        public SubscriptionEndpoint Endpoint
        {
            get
            {
                return _endpoint;
            }
            internal set
            {
                _endpoint = value;
            }
        }

        internal void StartInternal()
        {
            OnStart();

            Started = true;
        }

        internal void StopInternal()
        {
            OnStop();
            Started = false;        
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnStop()
        {
        }

        public bool Started
        {
            get;
            private set;
        }

        /// <summary>
        /// Handles sending a message to a subscriber endpoint.
        /// </summary>
        public abstract void Dispatch(MessageDelivery messageDelivery);

        /// <summary>
        /// Dispose any resources held by this dispatcher.
        /// </summary>
        /// <param name="disposing">Indicates whether this method is being called as a result of a direct call to Dispose (true) or by the object's finalizer (false).</param>
        protected virtual void Dispose(bool disposing)
        {
            
        }
        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        ~Dispatcher()
        {
            Dispose(false);
        }
    }
	
}
