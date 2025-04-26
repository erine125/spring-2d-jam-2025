using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WeedPlant : Plant
{
    public override void Shear()
    {
        Destroy(gameObject);
        // TODO: Shear SFX
    }
}
