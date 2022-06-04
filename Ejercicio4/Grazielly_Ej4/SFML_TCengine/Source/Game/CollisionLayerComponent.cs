using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCEngine;

namespace TCGame
{
    public class CollisionLayerComponent : BaseComponent
    {

        public enum ECollisionLayers
        {
            Player,
            Enemy,
            Person
        }

        private ECollisionLayers m_Layer;

        public ECollisionLayers Layer
        {
            get => m_Layer;
        }

        public CollisionLayerComponent(ECollisionLayers _layer)
        {
            m_Layer = _layer;
        }

        public override EComponentUpdateCategory GetUpdateCategory()
        {
            return EComponentUpdateCategory.Update;
        }

        public override object Clone()
        {
            CollisionLayerComponent clonedComponent = new CollisionLayerComponent(m_Layer);
            return clonedComponent;
        }

    }
}
