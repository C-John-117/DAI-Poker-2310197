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

        { // Cette partie je l'ai faite avec mon Camarade Jordy, nous avons mis nos connaissances en commun

            List<Player> list = new List<Player>();
            long x = 0;
            long y;

            foreach (Player player in activePlayers)
            {
                List<long> nbHandValue = new List<long>();

                Hand handPlayer = new PokerHand();
                PokerHand handTemp = new PokerHand();

                foreach (Card card1 in player.GetHand().GetAllCards())
                {
                    handTemp.AddCard(card1);
                }

                foreach (Card card in commonPool)
                {
                    handTemp.AddCard(card);
                }

                for (int i = 0; i <= 6; i++)
                {
                    handPlayer.AddCard(handTemp.GetCardX(i));
                    for (int j = i + 1; j <= 6; j++)
                    {
                        handPlayer.AddCard(handTemp.GetCardX(j));

                        for (int k = j + 1; k <= 6; k++)
                        {
                            handPlayer.AddCard(handTemp.GetCardX(k));

                            for (int l = k + 1; l <= 6; l++)
                            {
                                handPlayer.AddCard(handTemp.GetCardX(l));

                                for (int m = l + 1; m <= 6; m++)
                                {
                                    handPlayer.AddCard(handTemp.GetCardX(m));
                                    handPlayer.SortHand();
                                    nbHandValue.Add(handPlayer.CalculateHandValue());

                                    handPlayer.GetAllCards().RemoveAt(4);
                                }

                                handPlayer.GetAllCards().RemoveAt(3);
                            }

                            handPlayer.GetAllCards().RemoveAt(2);
                        }

                        handPlayer.GetAllCards().RemoveAt(1);
                    }

                    handPlayer.GetAllCards().RemoveAt(0);
                }
                nbHandValue.Sort();

                y = nbHandValue[nbHandValue.Count - 1];

                if (y > x)
                {
                    x = y;
                    list.Clear();
                    list.Add(player);
                }

                else if (y == x)
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
            AddCardToCommonPool(card, facedDown);

        }

        public override void DefineWinner()
        {
             
            List<Player> listJoueur = new List<Player>();
            foreach (Player player in playerOrder)
            {
                listJoueur.Add(player);
            }
            // le premier joueur est celui qui a la plus grande main


            UIManager.Instance.ChangeWinnerText(listJoueur, 00);
            if(listJoueur.Count == 1)
            {
                Player p;
                p = listJoueur[0];
               
                    UIManager.Instance.ShowWinner(p);
                    UIManager.Instance.EndRound();
            }
        }


        // demander si c correct 
        public override void GiveChipToWinners(List<Player> winners)
        {
            int montantAPartager = 0;
            montantAPartager = pot / winners.Count;

            foreach (Player player in winners)
            {
                player.AddToChipCount(montantAPartager);
            }
        }

        public override void Shuffle()
        {
            // List <Card> carteInitiale = initialCards;
            List<Card> carte_melangé = new List<Card>();
            int nbr_aleatoire;

            System.Random rnd = new System.Random();

            for (int i = 0; i < initialCards.Count; i++)
            {
                nbr_aleatoire = rnd.Next(0, initialCards.Count);
                if (carte_melangé.Contains(initialCards[nbr_aleatoire]))
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
