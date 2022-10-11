using System;
using System.Collections.Generic;

namespace Generics.Utility.Lock
{

    public class Locker
    {
        /// <summary> Locked or not </summary>
        public event Action<bool> LockStatusChanged;

        private List<string> _prohibitors = new List<string>();

        public bool IsLocked => _prohibitors.Count > 0;

        public void Lock(bool value, string id)
        {
            if (value)
            {
                if (_prohibitors.Contains(id))
                {
                    return;
                }

                _prohibitors.Add(id);

                if (_prohibitors.Count == 1)
                    LockStatusChanged?.Invoke(true);
            }
            else
            {
                if (!_prohibitors.Remove(id))
                    return;

                if (_prohibitors.Count == 0)
                    LockStatusChanged?.Invoke(false);
            }
        }

        public void Clear()
        {
            _prohibitors.Clear();

            LockStatusChanged?.Invoke(false);
        }

    }

}