using System.Collections.Generic;
using InputReader;
using Items;

namespace Player
{
    public class PlayerSystem
    {
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerBrain _playerBrain;
        public Inventory Inventory { get; }

        public PlayerSystem(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _playerBrain = new PlayerBrain(_playerEntity, inputSources);

            Inventory = new Inventory(null, null, _playerEntity.transform);
            // TODO: replace null
        }
    }
}