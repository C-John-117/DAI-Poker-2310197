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
            long valeurMain = 0;
            /*Note personnelle : 
             * Add ajoute un seul élement à la fois 
             * AddRange : ajoute une liste ou un tableau, 
             * donc une collection d'element d'un seul coup et l'ordre est gardé
             */

            List<Card> carteEnMain_handTotale = new List<Card>();

            carteEnMain_handTotale = GetAllCards();

            if (RoyalFlush(carteEnMain_handTotale) != 0)
            {
                valeurMain = RoyalFlush(carteEnMain_handTotale);
            }
            else if (StraightFlush(carteEnMain_handTotale) != 0)
            {
                valeurMain = Flush(carteEnMain_handTotale);
            }
            else if (FourOfAKind(carteEnMain_handTotale) != 0)
            {
                valeurMain = FourOfAKind(carteEnMain_handTotale);
            }
            else if (FullHouse(carteEnMain_handTotale) != 0)
            {
                valeurMain = FullHouse(carteEnMain_handTotale);
            }
            else if (FullHouse(carteEnMain_handTotale) != 0)
            {
                valeurMain = FullHouse(carteEnMain_handTotale);
            }
            else if (Flush(carteEnMain_handTotale) != 0)
            {
                valeurMain = Flush(carteEnMain_handTotale);
            }
            else if (Straight(carteEnMain_handTotale) != 0)
            {
                valeurMain = Straight((carteEnMain_handTotale));
            }
            else if (ThreeOfAKind(carteEnMain_handTotale) != 0)
            {
                valeurMain = ThreeOfAKind(carteEnMain_handTotale);
            }
            else if (TwoPair(carteEnMain_handTotale) != 0)
            {
                valeurMain = TwoPair(carteEnMain_handTotale);
            }
            else if (OnePair(carteEnMain_handTotale) != 0)
            {
                valeurMain = OnePair(carteEnMain_handTotale);
            }
            else if (HighHand(carteEnMain_handTotale) != 0)
            {
                valeurMain = HighHand(carteEnMain_handTotale);
            }
            return valeurMain;

        }

        public long RoyalFlush(List<Card> les5Cartes)
        {

            long valeurDeCetteMain = 0;
            bool qtTrouvé = false;
            //  long valeurDeCetteMain =0;

            if (StraightFlush(les5Cartes) != 0 && les5Cartes[4].GetMaxValue() == 14)

            {
                valeurDeCetteMain = 100000000;
                foreach (Card card in les5Cartes)
                {
                    valeurDeCetteMain += card.GetMaxValue();
                }
            }

            return valeurDeCetteMain;

        }

        // R.F = toutes les cartes de la même suit et dans l'ordre
        public long StraightFlush(List<Card> les5Cartes)
        {
            long valeurSF = 0;

            if (Straight(les5Cartes) != 0 && Flush(les5Cartes) != 0)
            {
                valeurSF = 90000000;
                foreach (Card card in les5Cartes)
                {
                    valeurSF += card.GetMaxValue();
                }

            }
            return valeurSF;

        }

        //F.O.A.K = 4 cartes de même valeur  

        public long FourOfAKind(List<Card> les5Cartes)
        {
            long valeurFOAK = 0;

            int compteur = 1;

            int valeurCarteDifferente = 0;


            // les5Cartes = les5Cartes; 
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
                     * Etant donné que nous sommes dans un FOAK, la carte differente des autres 
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
                valeurFOAK = 80000000 + valeurCarteDifferente + (valeurFOAK * 4);
            }

            return valeurFOAK;
        }


        public long FullHouse(List<Card> les5Cartes)
        {
            long valeurFH = 0;
            long TOAK = ThreeOfAKind(les5Cartes);
            long TP = TwoPair(les5Cartes);

            if (TOAK != 0 && TP != 0)
            {
                valeurFH = TOAK + TP + 70000000;
            }
            return valeurFH;
        }

        // La flush c'est 5 carte de même couleur 
        public long Flush(List<Card> les5Cartes)
        {
            long valeurF = 0;
            int commpteur = 1;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {

                if (les5Cartes[0].GetSuit() == les5Cartes[i + 1].GetSuit())
                {
                    commpteur++;

                }
            }

            if (commpteur == 5)
            {
                valeurF = 60000000;
                foreach (Card carte in les5Cartes)
                {
                    valeurF = valeurF + carte.GetMaxValue();
                }
            }

            return valeurF;
        }

        public long Straight(List<Card> les5Cartes)
        {
            long valeurS = 0;
            int compteur = 1;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                if (les5Cartes[i + 1].GetMaxValue() - les5Cartes[i].GetMaxValue() == 1)
                {
                    compteur++;
                }
            }

            if (compteur == 5)
            {
                valeurS = 50000000;
                foreach (Card carte in les5Cartes)
                {
                    valeurS = valeurS + carte.GetMaxValue();
                }
            }

            return valeurS;
        }

        public long ThreeOfAKind(List<Card> les5Cartes)
        {
            long valeur = 0;


            bool TOAKTrouvé = false;
            int valeurCarteDiferrente1 = 0;
            int valeurCarteDiferrente2 = 0;
            int valeurCarteIdentiques = 0;


            for (int i = 0; i < 3; i++)
            {
                TOAKTrouvé = false;
                if (les5Cartes[i].GetSuit() == les5Cartes[i + 1].GetSuit() && les5Cartes[i + 1].GetSuit() == les5Cartes[i + 2].GetSuit())
                {
                    TOAKTrouvé = true;
                    valeurCarteIdentiques = les5Cartes[i].GetMaxValue();

                }
                if (i == 0 && TOAKTrouvé == true)
                {
                    valeurCarteDiferrente1 = les5Cartes[3].GetMaxValue();
                    valeurCarteDiferrente2 = les5Cartes[4].GetMaxValue();
                }
                else if (i == 1 && TOAKTrouvé == true)
                {
                    valeurCarteDiferrente1 = les5Cartes[0].GetMaxValue();
                    valeurCarteDiferrente2 = les5Cartes[4].GetMaxValue();
                }
                else if (i == 2 && TOAKTrouvé == true)
                {
                    valeurCarteDiferrente1 = les5Cartes[0].GetMaxValue();
                    valeurCarteDiferrente2 = les5Cartes[1].GetMaxValue();
                }

            }

            if (TOAKTrouvé == true)
            {
                valeur = 40000000 + valeurCarteDiferrente1 + valeurCarteDiferrente2 + valeurCarteIdentiques;
            }


            return valeur;
        }
        public long TwoPair(List<Card> les5Cartes)
        {
            int ValeurPaire1 = 0;
            int ValeurPaire2 = 0;
            long valeurDeLaMain = 0;

            /* 
             *   indexPaire représente l'index de la carte suivant la deuxeime carte de la premiere paire trouvé, de ce fait dans mon deu - 
                 xième for , la boucle commencera à partir de la carte suivante la 1iere pere, evitant ainsi de revenir sur la paire ou
                 les deux cartes de la paire trouvé .
            *
            */
            int indexPaire = 0;

            bool TPTrouvé = false, paire1Trouvé = false, paire2Trouvé = false;

            for (int i = 0; i < les5Cartes.Count - 1 && paire1Trouvé == false; i++)
            {
                if (les5Cartes[i].GetMaxValue() == les5Cartes[i + 1].GetMaxValue())
                {
                    ValeurPaire1 = les5Cartes[(i + 1)].GetMaxValue();
                    indexPaire = i + 2;
                    paire1Trouvé = true;
                }
            }

            for (int j = indexPaire; j < les5Cartes.Count - 2 && paire2Trouvé == false; j++)
            {
                if (les5Cartes[j].GetMaxValue() == les5Cartes[j + 1].GetMaxValue())
                {
                    ValeurPaire2 = les5Cartes[(j + 1)].GetMaxValue();
                    paire2Trouvé = true;
                }
            }

            if (TPTrouvé == true)
            {
                valeurDeLaMain = 30000000 + (ValeurPaire1 * 2) + (ValeurPaire1 * 2);
            }
            return valeurDeLaMain;
        }

        public long OnePair(List<Card> les5Cartes)
        {
            int ValeurDeLaPaire = 0;
            bool HHTrouvé = false;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                if (les5Cartes[i].GetSuit() == les5Cartes[i + 1].GetSuit())
                {
                    ValeurDeLaPaire = les5Cartes[(i + 1)].GetMaxValue();
                    HHTrouvé = true;
                }
            }
            if (HHTrouvé == true)
            {
                ValeurDeLaPaire = 20000000 + ValeurDeLaPaire;
            }
            return ValeurDeLaPaire;

        }

        public long HighHand(List<Card> les5Cartes)
        {
            int ValeurCarteLaPlusHaute = 0;
            bool HHTrouvé = false;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                if (les5Cartes[i].GetMaxValue() < les5Cartes[i + 1].GetMaxValue())
                {
                    ValeurCarteLaPlusHaute = les5Cartes[i + 1].GetMaxValue();
                    HHTrouvé = true;
                }
            }

            if (HHTrouvé == true)
            {
                ValeurCarteLaPlusHaute = 10000000 + ValeurCarteLaPlusHaute;

            }

            return ValeurCarteLaPlusHaute;
        }
    }
}
