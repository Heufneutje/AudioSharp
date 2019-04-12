using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AudioSharp.Utils
{
    [Serializable]
    public abstract class BaseChangeTrackable
    {
        [NonSerialized]
        private Dictionary<string, object> _originalValues;

        [NonSerialized]
        private Dictionary<string, object> _changedValues;

        [NonSerialized]
        private bool _hasChangesCache;

        public void StartTracking()
        {
            _originalValues = new Dictionary<string, object>();

            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo property in properties)
                if (!property.GetCustomAttributes(typeof(ChangeTrackingIgnoreAttribute), true).Any())
                    _originalValues.Add(property.Name, property.GetValue(this));
        }

        public Dictionary<string, object> GetChanges(bool refreshChanges = false)
        {
            if (_originalValues == null)
                _originalValues = new Dictionary<string, object>();

            if (!_hasChangesCache || refreshChanges)
            {
                _changedValues = new Dictionary<string, object>();

                foreach (PropertyInfo property in GetType().GetProperties())
                    if (_originalValues.ContainsKey(property.Name) && !Equals(property.GetValue(this, null), _originalValues[property.Name]))
                        _changedValues.Add(property.Name, property.GetValue(this));

                _hasChangesCache = true;
            }

            return _changedValues;
        }

        public bool HasPropertyChanged(string propertyName, bool refreshChanges = false)
        {
            return GetChanges(refreshChanges).ContainsKey(propertyName);
        }
    }
}
