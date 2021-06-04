using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeySafe.Controller
{
    class PasswordGenerator
    {
        // Eingaben des Benutzers
        private bool wishDefinitelyUse, isEnoughChecked;
        private string specialWishes;
        private int passwordLength;

        //Passwort
        private string PossibleCharacters;
        private Random random = new Random();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="lowerCase">Checked Kleinbuchstaben</param>
        /// <param name="upperCase">Checked Grossbuchstaben</param>
        /// <param name="specialCharacters">Checked Spezialzeichen</param>
        /// <param name="spaces">Checked Leehrzeichen</param>
        /// <param name="numbers">Checked Nummern</param>
        /// <param name="wishDefinitelyUse">Alle Zeichenwuensche im Passwort verwenden</param>
        /// <param name="specialWishes">Speziele Zeichenwuensche</param>
        /// <param name="passwordLength">Passwort laenge</param>
        public PasswordGenerator(bool? lowerCase, bool? upperCase, bool? specialCharacters, bool? spaces, bool? numbers, bool? wishDefinitelyUse, string specialWishes, string passwordLength)
        {
            if (lowerCase ?? false)
            {
                PossibleCharacters += "abcdefghijklmnopqrstuvwxyz";
                isEnoughChecked = true;
            }

            if (upperCase ?? false)
            {
                PossibleCharacters += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                isEnoughChecked = true;
            }

            if (specialCharacters ?? false)
            {
                PossibleCharacters += @"°§+*%&/()=?`!;:_~^£-<>\¦@#°¬|´[]{}¨";
                isEnoughChecked = true;
            }

            if (numbers ?? false)
            {
                PossibleCharacters += "0123456789";
                isEnoughChecked = true;
            }

            if (spaces ?? false)
                PossibleCharacters += " ";

            if (!string.IsNullOrWhiteSpace(specialWishes) && wishDefinitelyUse == false)
            {
                this.specialWishes = specialWishes;
            }
            else
            {
                this.specialWishes = specialWishes;
                this.wishDefinitelyUse = true;
                isEnoughChecked = true;
            }


            int.TryParse(passwordLength, out this.passwordLength);
        }

        /// <summary>
        /// Passwort Generieren
        /// </summary>
        /// <returns>Passwort</returns>
        public string GeneratePassword()
        {
            if (isEnoughChecked)
            {
                if (wishDefinitelyUse == true)
                    return WishDefinitelyUse();

                else
                    return new string(Enumerable.Repeat(PossibleCharacters, passwordLength)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            else { return null; }
        }

        /// <summary>
        /// Passwort Generieren indem aufjedenfall die gewuenschten Zeichen vorkommen
        /// </summary>
        /// <returns>Passwort</returns>
        private string WishDefinitelyUse()
        {
            Random zufall = new Random();

            // Eine char Liste erstellen von denn Gewuenschten Zeichen
            List<char> specialWishesList = new List<char>(specialWishes);
            List<char> passwordList;
            if (specialWishesList.Count < passwordLength)
            {
                // Sind keine Random Cahracter gesetzt
                if (PossibleCharacters != null)
                {
                    // Ein Passwort generieren in der angegeben laenge minus die Gewuenschten zeichen
                    passwordList = new List<char>(new string(Enumerable.Repeat(PossibleCharacters, (passwordLength - specialWishesList.Count))
                        .Select(s => s[random.Next(s.Length)]).ToArray()));
                }

                // Das Passwort bestehet nur aus den Wunschcaharactern
                else
                {
                    // Ein Passwort generieren in der angegeben laenge minus die Gewuenschten zeichen
                    passwordList = new List<char>(new string(Enumerable.Repeat(specialWishes, (passwordLength - specialWishesList.Count))
                        .Select(s => s[random.Next(s.Length)]).ToArray()));
                }

                // Erwuenschte Zeichen zufaellig in das Passwort einsetzen
                foreach (char c in specialWishesList)
                    passwordList.Insert(zufall.Next(0, passwordList.Count), c);
                return new string(passwordList.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}
