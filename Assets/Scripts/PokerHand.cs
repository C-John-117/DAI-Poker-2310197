using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardTemplate
{
    /// <summary>
    /// Implémentation de la classe Hand version Poker
    /// </summary>
    [Serializable]
    public class PokerHand : Hand
    {
        public override long CalculateHandValue()
        {
            /*Note personnelle : 
             * Add ajoute un seul élement à la fois 
             * AddRange : ajoute une liste ou un tableau, 
             * donc une collection d'element d'un seul coup et l'ordre est gardé
             */

            /*       public virtual void SortHand()
            {
            cards.Sort();
            }

            public List<Card> GetAllCards()
            {
            return cards;
            }

                    */

            List<Card> carteEnMain_handTotale = new List<Card>();

            carteEnMain_handTotale = GetAllCards();

            return 0;

        }

        public long QuinteFlush(List<Card> les7Cartes)
        {
            // trie par ordre croissant
            les7Cartes.Sort();
            long valeurDeCetteMain=0;
            List<Card> suitIdentique = new List<Card>();

            if (les7Cartes[6].GetMaxValue() == 10) 
            {
                //for (int i = 0; i < les7Cartes.Count - 1; i++)
                for (int i = 0; i < 4; i++)
                {
                    // si c'est de même symbole ,
                    if (les7Cartes[i].GetSuit() == les7Cartes[i + 1].GetSuit())
                    {
                        if (i == 3 && les7Cartes[i].GetSuit() == les7Cartes[i + 1].GetSuit())
                        {
                            suitIdentique.Add(les7Cartes[i + 1]);

                        }
                        suitIdentique.Add(les7Cartes[i]);
                    }
                }

                foreach (Card card in suitIdentique)
                {
                    valeurDeCetteMain = card.GetMaxValue() + valeurDeCetteMain;
                }
            }

            return valeurDeCetteMain;
        }
    }
}
