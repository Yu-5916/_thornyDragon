using UnityEngine;
using System.Collections.Generic;


namespace Ardunity
{
	[AddComponentMenu("ARDUnity/Reactor/Effect/LightIntensityReactor")]
    [HelpURL("https://sites.google.com/site/ardunitydoc/references/reactor/lightintensityreactor")]
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleToggle : ArdunityReactor
	{
        private bool appear;
        private ParticleSystem _particle;

        private IWireInput<float> _analogInput;
        private IWireOutput<float> _analogOutput;
        private IWireInput<bool> _digitalInput;
		private IWireOutput<bool> _digitalOutput;

        public bool Appear
        {
            get
            {
                _particle.Play(true);
                return appear;
            }

            set
            {

                appear = true;
            }
        }

        protected override void Awake()
		{
            base.Awake();
            
			_particle = GetComponent<ParticleSystem>();
		}

		// Use this for initialization
		void Start ()
		{
		
		}
		
		void OnEnable()
		{
				
			if(_digitalInput != null)
			{
                if (_digitalInput.input)
                    _particle.Play(true);
                else
                    _particle.Play(false);
			}
		}
		
		// Update is called once per frame
		void Update ()
		{			
			if(_digitalOutput != null)
			{
                if (appear)
					_digitalOutput.output = false;
				else
					_digitalOutput.output = true;
			}
		}

        private void OnAnalogInputChanged(float value)
        {
            if (!this.enabled)
                return;
        }
            private void OnDigitalInputChanged(bool value)
		{
			if(!this.enabled)
				return;

            if (_digitalInput.input)
                _particle.Play(true);
            else
                _particle.Play(true);
		}
		
		protected override void AddNode(List<Node> nodes)
        {
			base.AddNode(nodes);
			
            nodes.Add(new Node("setActive", "Set Active", typeof(IWireInput<bool>), NodeType.WireFrom, "Input<bool>"));
			nodes.Add(new Node("getActive", "Get Active", typeof(IWireOutput<bool>), NodeType.WireFrom, "Output<bool>"));
			nodes.Add(new Node("setIntensity", "Set Intensity", typeof(IWireInput<float>), NodeType.WireFrom, "Input<float>"));
			nodes.Add(new Node("getIntensity", "Get Intensity", typeof(IWireOutput<float>), NodeType.WireFrom, "Output<float>"));
        }
        
        protected override void UpdateNode(Node node)
        {
            if(node.name.Equals("setActive"))
            {
                node.updated = true;
                if(node.objectTarget == null && _digitalInput == null)
                    return;
                
                if(node.objectTarget != null)
                {
                    if(node.objectTarget.Equals(_digitalInput))
                        return;
                }
                
                if(_digitalInput != null)
                    _digitalInput.OnWireInputChanged -= OnDigitalInputChanged;
                
                _digitalInput = node.objectTarget as IWireInput<bool>;
                if(_digitalInput != null)
                    _digitalInput.OnWireInputChanged += OnDigitalInputChanged;
                else
                    node.objectTarget = null;
                
                return;
            }
            else if(node.name.Equals("getActive"))
            {
                node.updated = true;
                if(node.objectTarget == null && _digitalOutput == null)
                    return;
                
                if(node.objectTarget != null)
                {
                    if(node.objectTarget.Equals(_digitalOutput))
                        return;
                }
                
                _digitalOutput = node.objectTarget as IWireOutput<bool>;
                if(_digitalOutput == null)
                    node.objectTarget = null;
                
                return;
            }
            else if(node.name.Equals("setIntensity"))
            {
                node.updated = true;
                if(node.objectTarget == null && _analogInput == null)
                    return;
                
                if(node.objectTarget != null)
                {
                    if(node.objectTarget.Equals(_analogInput))
                        return;
                }
                
                if(_analogInput != null)
                    _analogInput.OnWireInputChanged -= OnAnalogInputChanged;
                
                _analogInput = node.objectTarget as IWireInput<float>;
                if(_analogInput != null)
                    _analogInput.OnWireInputChanged += OnAnalogInputChanged;
                else
                    node.objectTarget = null;
                
                return;
            }
            else if(node.name.Equals("getIntensity"))
            {
                node.updated = true;
                if(node.objectTarget == null && _analogOutput == null)
                    return;
                
                if(node.objectTarget != null)
                {
                    if(node.objectTarget.Equals(_analogOutput))
                        return;
                }
                
                _analogOutput = node.objectTarget as IWireOutput<float>;
                if(_analogOutput == null)
                    node.objectTarget = null;
                
                return;
            }
            
            base.UpdateNode(node);
        }
	}
}
