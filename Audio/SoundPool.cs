using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer_MonoG.Audio
{
    public class SoundPool : ICollection<SoundEffect>
    {

        private readonly List<SoundEffect> _sounds = new List<SoundEffect>();

        private readonly Random _random = new Random();

        public int Count => ((ICollection<SoundEffect>)_sounds).Count;

        public bool IsReadOnly => ((ICollection<SoundEffect>)_sounds).IsReadOnly;




        public SoundPool()
        {
            
        }




        public void Add(SoundEffect item)
        {
            ((ICollection<SoundEffect>)_sounds).Add(item);
        }

        public void Clear()
        {
            ((ICollection<SoundEffect>)_sounds).Clear();
        }

        public bool Contains(SoundEffect item)
        {
            return ((ICollection<SoundEffect>)_sounds).Contains(item);
        }

        public void CopyTo(SoundEffect[] array, int arrayIndex)
        {
            ((ICollection<SoundEffect>)_sounds).CopyTo(array, arrayIndex);
        }

        public IEnumerator<SoundEffect> GetEnumerator()
        {
            return ((IEnumerable<SoundEffect>)_sounds).GetEnumerator();
        }

        public bool Remove(SoundEffect item)
        {
            return ((ICollection<SoundEffect>)_sounds).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_sounds).GetEnumerator();
        }

        public void PlayRandom(float volume, float pitch, float pan)
        {

            int randomInt = _random.Next(_sounds.Count);

            _sounds[randomInt].Play(volume,pitch, pan);

        }


        public void PlayRandom()
        {

            int randomInt = _random.Next(_sounds.Count);

            _sounds[randomInt].Play();

        }
    }
}
