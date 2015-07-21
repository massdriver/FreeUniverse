using FreeUniverse.Common.Arch;
using FreeUniverse.Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeUniverse.Common.World
{
    public class WorldController : IBaseObject, IStateSerializable
    {
        public int layer { get; private set; }
        protected NetworkMessageHandler messageHandler { get; set; }
        protected Dictionary<uint, Solar> solars { get; set; }
        protected uint lastSolarID { get; set; }

        public WorldController(int layer)
        {
            this.lastSolarID = 0;
            this.layer = layer;
            this.messageHandler = new NetworkMessageHandler();
            this.solars = new Dictionary<uint, Solar>();

            SetupNetworkMessageHandlers();
        }

        public virtual void SetupNetworkMessageHandlers()
        {

        }

        public virtual Solar GetSolar(uint id)
        {
            Solar s = null;

            if (solars.TryGetValue(id, out s))
                return s;

            return null;
        }

        public virtual Solar CreateSolar(ArchSolar obj)
        {
            Solar solar = new Solar(this, ++lastSolarID, obj);

            solars[solar.id] = solar;

            return solar;
        }

        public virtual void Update(float dt)
        {
            foreach (KeyValuePair<uint, Solar> e in solars)
                e.Value.Update(dt);
        }

        public virtual void Init()
        {
            throw new NotImplementedException();
        }

        public virtual void Release()
        {
            throw new NotImplementedException();
        }

        public virtual ValueMap GetState()
        {
            throw new NotImplementedException();
        }

        public virtual void SetState(ValueMap map)
        {
            throw new NotImplementedException();
        }
    }
}
