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
            // Cette fonction je l'avais faite avant que vous ne nous disiez de ne travailler qu'avec une main de 5 cartes
            // j'ai toute fois d�cider de la garder pour continuer d'avancer 

            long valeurDeCetteMain = 0;
            bool qtTrouv� = false;
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
                    if (suitIdentique.Count == 4 || i == 5)
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
                qtTrouv� = true;
            }
            if (qtTrouv� == true)
            {
                valeurDeCetteMain = 100000000 + valeurDeCetteMain;
            }
            return valeurDeCetteMain;
        }

        // S.F = toutes les cartes de la m�me suit et dans l'ordre
        public long StraightFlush(List<Card> les5Cartes)
        {
            /*
             * Recevant une liste tri�, si la premiere carte est diff�rent de toutes les autres, alors 
            / d'office nous ne sommes pas en pr�sence d'un StraightFlush
            */

            long valeurStraight = 0;
            int compteur = 1;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {

                if (les5Cartes[0].GetSuit() == les5Cartes[i + 1].GetSuit())
                {
                    compteur = compteur + 1;

                }
            }

            // Si mon compteur = 5 cela voudrait dire que toutes les 5 cartes sont de m�me couleur

            if (compteur == 5)
            {
                if (les5Cartes[0].GetMaxValue() == les5Cartes[4].GetMaxValue() + 4)
                {
                    foreach (Card card in les5Cartes)
                    {
                        valeurStraight += card.GetMaxValue();
                    }
                    valeurStraight = 90000000 + valeurStraight;
                }
            }
            return valeurStraight;

        }

        //F.O.A.K = 4 cartes de m�me valeur  

        public long FourOfAKind(List<Card> les5Cartes)
        {
            long valeurFOAK =0;

            int compteur = 1;

            int valeurCarteDifferente=0;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                if (les5Cartes[i].GetMaxValue() == les5Cartes[i + 1].GetMaxValue())
                {
                    compteur++;
                    valeurFOAK = les5Cartes[i].GetMaxValue();
                }
                else
                {
                    /*
                     * Etant donn� que nous sommes dans un FOAK, la carte differente des autres 
                     * sera soit la premiere carte, soit la derniere carte.
                     */
                    if (i == 0)
                    {
                        valeurCarteDifferente = les5Cartes[0].GetMaxValue();
                    }
                    else if (i == 3)
                    {
                        valeurCarteDifferente = les5Cartes[4].GetMaxValue();
                    }
                }
            }

            if (compteur == 4)
            {
                valeurFOAK = 80000000  + valeurCarteDifferente + (valeurFOAK * 4);
            }
            return valeurFOAK;
        }
    }
}
