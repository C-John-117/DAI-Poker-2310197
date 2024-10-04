using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardTemplate
{
    /// <summary>
    /// Impl�mentation de la classe Hand version Poker
    /// </summary>
    [Serializable]
    public class PokerHand : Hand
    {
        public override long CalculateHandValue()
        {
            long valeurMain = 0;
            /*Note personnelle : 
             * Add ajoute un seul �lement � la fois 
             * AddRange : ajoute une liste ou un tableau, 
             * donc une collection d'element d'un seul coup et l'ordre est gard�
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

            valeurMain = QuinteFlush(carteEnMain_handTotale);

            return 0;

        }

        public long QuinteFlush(List<Card> les7Cartes)
        {
            // Avec SortHand, les cartes sont d�ja tri�es par ordre croissant

            long valeurDeCetteMain = 7000000;
          //  long valeurDeCetteMain =0;
            List<Card> suitIdentique = new List<Card>();


            //for (int i = 0; i < les7Cartes.Count - 1; i++)
            for (int i = 0; i < les7Cartes.Count - 1; i++)
            {
                // si c'est de m�me symbole on ajoute dans la liste
                if (les7Cartes[i].GetSuit() == les7Cartes[i + 1].GetSuit())
                {
                    suitIdentique.Add(les7Cartes[i]);

                    // pour ajouter le dernier element de la liste si il doit l'�tre
                    if (suitIdentique.Count == 4 || i==5)
                    {
                        suitIdentique.Add(les7Cartes[i + 1]);
                        break;
                    }
                }
                else
                {
                    suitIdentique.Clear();
                }
            }

            foreach (Card card in suitIdentique)
            {
                valeurDeCetteMain = card.GetMaxValue() + valeurDeCetteMain;
            }
            return valeurDeCetteMain;
        }
    }
}
