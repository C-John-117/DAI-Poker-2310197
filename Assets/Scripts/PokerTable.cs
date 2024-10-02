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
            List<Player> list = new List<Player>();
            long x = 0;
            long y;

            foreach(Player player in activePlayers)
            {
                Hand handPlayer = new PokerHand();

                handPlayer = player.GetHand();

                foreach(Card carte in player.GetHand().GetAllCards())
                {
                    handPlayer.AddCard(carte);
                }
                // creer un hand pour les cartes que le joueur a en main

                foreach (Card card in commonPool)
                {
                    handPlayer.AddCard(card);
                }
                handPlayer.SortHand();
                y = handPlayer.CalculateHandValue();

                if(y > x)
                {
                    x = y;
                    list.Clear();
                    list.Add(player);
                }

                else if(y == x)
                {
                    list.Add(player);
                }
                
            }

            return list;
        }

     

        public override void Deal(Card card, Player player, bool facedDown)
        {
            player.AddCardToHand(card);
        }

        public override void Deal(Card card, bool facedDown)
        {
            AddCardToCommonPool(card,facedDown);
          
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
           // List <Card> carteInitiale = initialCards;
            List<Card> carte_melangé = new List<Card>();
            int nbr_aleatoire ; 

            System.Random rnd = new System.Random();

            for (int i = 0; i < initialCards.Count; i++)
            {
                nbr_aleatoire = rnd.Next(0,initialCards.Count);
                if (carte_melangé.Contains( initialCards[nbr_aleatoire]))
                {

                }
                else
                {
                    carte_melangé.Add(initialCards[nbr_aleatoire]);
                }
            }


            foreach (Card card in carte_melangé)
            {

                deck.Push(card);
            }
        }

        public override void UpdatePot(int amount)
        {
            pot = pot + amount;
        }
    }
}
