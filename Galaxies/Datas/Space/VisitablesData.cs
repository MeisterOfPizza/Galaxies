using Galaxies.Controllers;
using Galaxies.Datas.Helpers;
using Galaxies.Extensions;
using Galaxies.Space;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Visitables")]
    public class VisitablesData : Data
    {

        [XmlArray("Pointers", IsNullable = true)]
        [XmlArrayItem("Pointer", typeof(DataPointer), IsNullable = true)]
        public DataPointer[] VisitablePointers { get; set; }

        public IVisitable GetRandom()
        {
            var pointer = VisitablePointers[Random.Next(VisitablePointers.Length)];

            if (pointer.Type == DataPointer.VISITABLE_TYPE_PLANETARYSYSTEM)
            {
                return new PlanetarySystem(DataController.LoadData<PlanetarySystemData>(pointer.Id, DataFileType.PlanetarySystems));
            }
            else
            {
                return null;
            }
        }

    }

}
