using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        /// </summary>
        /// <param name="les5Cartes">Liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>Retourne une valeur si la main est une Royal Flush, sinon retourne 0.</returns>
        public long RoyalFlush(List<Card> les5Cartes)
        {

            long valeurDeCetteMain = 0;

            // Avec cette condition je verifie si la main est une Straight Flush et si la carte la plus haute est un As (valeur 14)
            if (StraightFlush(les5Cartes) != 0 && les5Cartes[4].GetMaxValue() == 14)

            {
                valeurDeCetteMain = 100000000;

                // Je somme la valeur de chaque carte pour obtenir la valeur totale de cette main
                foreach (Card card in les5Cartes)
                {
                    valeurDeCetteMain += card.GetMaxValue();
                }
            }

            return valeurDeCetteMain;

        }

        /// <summary>
        /// Une Straight Flush est une main compos�e de 5 cartes cons�cutives de la m�me couleur.
        /// </summary>
        /// <param name="les5Cartes">Liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>Retourne une valeur si la main est une Straight Flush, sinon retourne 0.</returns>
        public long StraightFlush(List<Card> les5Cartes)
        {
            long valeurSF = 0;

            // Je v�rifie si la main est � la fois une Straight (suite) et une Flush (couleur)
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


        /// <summary>
        /// Un Four of a Kind est une main compos�e de quatre cartes de la m�me valeur et d'une carte diff�rente.
        /// Si la main est un Four of a Kind, une valeur sp�cifique est attribu�e.
        /// </summary>
        /// <param name="les5Cartes">Liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>Retourne une valeur �lev�e si la main est un Four of a Kind, sinon retourne 0.</returns>
        public long FourOfAKind(List<Card> les5Cartes)
        {
            long valeurFOAK = 0;

            int compteur = 1;

            int valeurCarteDifferente = 0;


            // Parcours  toutes les cartes de la liste pour voir si les cartes sont de m�me valeurs 

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                // Si la carte actuelle a la m�me valeur que la suivante
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

            // Si le compteur atteint 4, cela signifie qu'il y a bien quatre cartes de m�me valeur
            if (compteur == 4)
            {
                valeurFOAK = 80000000 + valeurCarteDifferente + (valeurFOAK * 4);
            }

            return valeurFOAK;
        }

        /// <summary>
        /// Un Full House est compos� d'un brelan (trois cartes de m�me valeur) et d'une paire (deux cartes de m�me valeur).
        /// </summary>
        /// <param name="les5Cartes">Liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne une valeur num�rique repr�sentant la force du Full House si la main en est un,
        /// sinon retourne 0. 
        /// </returns>
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

        /// <summary>
        /// Un flush est une main o� toutes les cartes ont la m�me couleur.
        /// </summary>
        /// <param name="les5Cartes">Liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne une valeur  repr�sentant la force de la Flush si la main en est une, sinon retourne 0.
        /// </returns>
        public long Flush(List<Card> les5Cartes)
        {
            long valeurF = 0;
            int commpteur = 1;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                // Je compare la couleur de la premiere carte avec toutes les autres car il suffit
                // qu'une soit diff�rente pour que la main ne soit pas une flush

                if (les5Cartes[0].GetSuit() == les5Cartes[i + 1].GetSuit())
                {
                    commpteur++;

                }
            }

            // Si toutes les cartes ont la m�me couleur (compteur = 5), on a une flush
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

        /// <summary>
        /// Une Straight est une main o� les valeurs des cartes se suivent de mani�re cons�cutive.
        /// </summary>
        /// <param name="les5Cartes">Liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne une valeur repr�sentant la force de la suite si la main en est une, sinon retourne 0. 
        /// </returns>
        public long Straight(List<Card> les5Cartes)
        {
            long valeurS = 0;
            int compteur = 1;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {

                // Si la diff�rence de valeur entre les deux cartes est de 1, on incr�mente le compteur
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

        /// <summary>
        /// Un ThreeOfAKind est constitu� de trois cartes de m�me valeur.
        /// </summary>
        /// <param name="les5Cartes">Une liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne une valeur repr�sentant la force de la main si elle contient un ThreeOfAKind, sinon retourne 0.
        /// </returns>
        public long ThreeOfAKind(List<Card> les5Cartes)
        {
            long valeur = 0;


            bool TOAKTrouv� = false;
            int valeurCarteDiferrente1 = 0;
            int valeurCarteDiferrente2 = 0;
            int valeurCarteIdentiques = 0;

            // On parcourt les trois premi�res paires de cartes pour v�rifier si elles sont de m�me couleur
            for (int i = 0; i < 3; i++)
            {
                TOAKTrouv� = false;

                // Si les trois cartes cons�cutives ont la m�me valeur, on a trouv� un ThreeOfKind
                if (les5Cartes[i].GetSuit() == les5Cartes[i + 1].GetSuit() && les5Cartes[i + 1].GetSuit() == les5Cartes[i + 2].GetSuit())
                {
                    TOAKTrouv� = true;
                    valeurCarteIdentiques = les5Cartes[i].GetMaxValue();

                }
                // Si on a trouv� 3 cartes de m�me couleur  et qu'on est au premier tour, on r�cup�re les valeurs des deux derni�res cartes
                if (i == 0 && TOAKTrouv� == true)
                {
                    valeurCarteDiferrente1 = les5Cartes[3].GetMaxValue();
                    valeurCarteDiferrente2 = les5Cartes[4].GetMaxValue();
                }
                // Si on a trouv� 3 cartes de m�me couleur et qu'on est � la deuxi�me it�ration ou deuxieme tour, on r�cup�re les valeurs de la premi�re et de la derni�re carte
                else if (i == 1 && TOAKTrouv� == true)
                {
                    valeurCarteDiferrente1 = les5Cartes[0].GetMaxValue();
                    valeurCarteDiferrente2 = les5Cartes[4].GetMaxValue();
                }
                // Si on a trouv� 3 cartes de m�me couleur et qu'on est � la troisi�me it�ration , on r�cup�re les valeurs des 2 premi�res cartes

                else if (i == 2 && TOAKTrouv� == true)
                {
                    valeurCarteDiferrente1 = les5Cartes[0].GetMaxValue();
                    valeurCarteDiferrente2 = les5Cartes[1].GetMaxValue();
                }

            }

            if (TOAKTrouv� == true)
            {
                valeur = 40000000 + valeurCarteDiferrente1 + valeurCarteDiferrente2 + valeurCarteIdentiques;
            }


            return valeur;
        }

        /// <summary>
        /// Ici on d�termine si une main de cinq cartes contient exactement deux paires.
        /// Une paire est constitu�e de deux cartes de m�me valeur. 
        /// </summary>
        /// <param name="les5Cartes">Une liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne une valeur  repr�sentant la force de la main si elle contient deux paires, sinon retourne 0.
        /// </returns>
        public long TwoPair(List<Card> les5Cartes)
        {
            int ValeurPaire1 = 0;
            int ValeurPaire2 = 0;
            long valeurDeLaMain = 0;

            /* 
             *   indexPaire repr�sente l'index de la carte suivant la deuxeime carte de la premiere paire trouv�, de ce fait dans mon deu - 
                 xi�me for , la boucle commencera � partir de la carte suivante la 1iere pere, evitant ainsi de revenir sur la paire ou
                 les deux cartes de la paire trouv� .
            *
            */
            int indexPaire = 0;

            bool TPTrouv� = false, paire1Trouv� = false, paire2Trouv� = false;

            for (int i = 0; i < les5Cartes.Count - 1 && paire1Trouv� == false; i++)
            {
                // Si on trouve deux cartes cons�cutives de m�me valeur, on a trouv� la premi�re paire
                if (les5Cartes[i].GetMaxValue() == les5Cartes[i + 1].GetMaxValue())
                {
                    ValeurPaire1 = les5Cartes[(i + 1)].GetMaxValue();
                    indexPaire = i + 2; // indexPaire permettra de commencer la recherche de la deuxi�me paire � partir de la carte suivante
                    paire1Trouv� = true;
                }
            }

            for (int j = indexPaire; j < les5Cartes.Count - 2 && paire2Trouv� == false; j++)
            {
                if (les5Cartes[j].GetMaxValue() == les5Cartes[j + 1].GetMaxValue())
                {
                    ValeurPaire2 = les5Cartes[(j + 1)].GetMaxValue();
                    paire2Trouv� = true;
                }
            }

            if (TPTrouv� == true)
            {
                valeurDeLaMain = 30000000 + (ValeurPaire1 * 2) + (ValeurPaire1 * 2);
            }
            return valeurDeLaMain;
        }

        /// <summary>
        /// D�termine si une main de cinq cartes contient exactement une paire.
        /// Une paire est constitu�e de deux cartes de m�me valeur.
        /// </summary>
        /// <param name="les5Cartes">Une liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne une valeur  repr�sentant la force de la main si elle contient deux paires, sinon retourne 0.
        /// </returns>
        public long OnePair(List<Card> les5Cartes)
        {
            int ValeurDeLaPaire = 0;
            bool HHTrouv� = false;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                if (les5Cartes[i].GetSuit() == les5Cartes[i + 1].GetSuit())
                {
                    ValeurDeLaPaire = les5Cartes[(i + 1)].GetMaxValue();
                    HHTrouv� = true;
                }
            }
            if (HHTrouv� == true)
            {
                ValeurDeLaPaire = 20000000 + ValeurDeLaPaire;
            }
            return ValeurDeLaPaire;

        }

        /// <summary>
        /// D�termine la carte la plus haute dans une main de cinq cartes.
        /// </summary>
        /// <param name="les5Cartes">Une liste de 5 cartes repr�sentant la main � �valuer.</param>
        /// <returns>
        /// Retourne la valeur de la carte la plus haute si elle est troub� et 0 si non.
        /// </returns>
        public long HighHand(List<Card> les5Cartes)
        {
            int ValeurCarteLaPlusHaute = 0;
            bool HHTrouv� = false;

            for (int i = 0; i < les5Cartes.Count - 1; i++)
            {
                if (les5Cartes[i].GetMaxValue() < les5Cartes[i + 1].GetMaxValue())
                {
                    ValeurCarteLaPlusHaute = les5Cartes[i + 1].GetMaxValue();
                    HHTrouv� = true;
                }
            }

            if (HHTrouv� == true)
            {
                ValeurCarteLaPlusHaute = 10000000 + ValeurCarteLaPlusHaute;

            }

            return ValeurCarteLaPlusHaute;
        }
    }
}
