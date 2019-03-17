using Galaxies.Controllers;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_PlayerShipTemplates
    {

        public string                        CurrentPlayerShipTemplateId { get; private set; }
        public SaveFile_PlayerShipTemplate[] Templates                   { get; private set; }

        public SaveFile_PlayerShipTemplates()
        {
            Templates = new SaveFile_PlayerShipTemplate[ShipyardController.PlayerShipTemplates.Length];

            for (int i = 0; i < Templates.Length; i++)
            {
                Templates[i] = new SaveFile_PlayerShipTemplate(ShipyardController.PlayerShipTemplates[i].Id, ShipyardController.PlayerShipTemplates[i].Unlocked);
            }

            CurrentPlayerShipTemplateId = ShipyardController.CurrentPlayerShipTemplate.Id;
        }

    }

}
