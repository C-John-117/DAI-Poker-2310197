using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace CardTemplate
{
    /// <summary>
    /// Implémentation de la classe Table version Poker
    /// </summary>
    public class PokerTable : Table
    {
        public override List<Player> CalculateWinner()
        {
            throw new NotImplementedException();
        }

        public override void Deal(Card card, Player player, bool facedDown)
        {
            throw new NotImplementedException();
        }

        public override void Deal(Card card, bool facedDown)
        {
            throw new NotImplementedException();
        }

        public override void DefineWinner()
        {
            throw new NotImplementedException();
        }

        public override void GiveChipToWinners(List<Player> winners)
        {
            throw new NotImplementedException();
        }

        public override void Shuffle()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePot(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
