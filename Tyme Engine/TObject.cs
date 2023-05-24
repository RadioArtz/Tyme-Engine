
using System.Diagnostics.Contracts;

namespace Tyme_Engine
{
    public class TObject
    {
        public bool IsPendingKill { get; private set; } = false;

        public void MarkPendingKill()
        {
            this.IsPendingKill = true;
            OnKill();
        }

        public virtual void OnKill()
        {

        }

        public static bool IsValid(TObject obj)
        {
            return (obj != null && !obj.IsPendingKill);
        }

    }
}
