namespace EcoSys.ConsoleApp.Validation;

public class Valid
{
        public string LerTextoObrigatorio(string prompt, int min = 1, int max = 100)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";
                if (input.Length >= min && input.Length <= max)
                {
                    return input;
                }
                Console.WriteLine($"Deve ter {min}-{max} caracteres!");
            }
        }

        public string LerEmailValido(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string email = Console.ReadLine()?.Trim() ?? "";

                if (email.Contains("@") && email.Contains(".") && email.Length > 5)
                    return email;

                Console.WriteLine("Email inválido! Use: usuario@dominio.com");
            }
        }

        public string LerLoginValido(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string login = Console.ReadLine()?.Trim() ?? "";

                if (login.Length >= 3 && login.Length <= 20 
                && !login.Contains(" "))
                    return login;

                Console.WriteLine("Login: 3-20 chars, sem espaços!");
            }
        }        
}
