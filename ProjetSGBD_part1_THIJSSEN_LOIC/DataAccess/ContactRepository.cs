using Npgsql;
using NpgsqlTypes;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class ContactRepository
    {
        private readonly string _connectionString;

        public ContactRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ajouter(Contact contact)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL ajouter_contact(
                    @nom,
                    @prenom,
                    @registre,
                    @adresse,
                    @gsm,
                    @mail
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@nom", NpgsqlDbType.Varchar).Value = contact.Nom;
            command.Parameters.Add("@prenom", NpgsqlDbType.Varchar).Value = contact.Prenom;
            command.Parameters.Add("@registre", NpgsqlDbType.Varchar).Value = contact.Registre;
            command.Parameters.Add("@adresse", NpgsqlDbType.Varchar).Value = contact.Adresse;
            command.Parameters.Add("@gsm", NpgsqlDbType.Varchar).Value = contact.Gsm;
            command.Parameters.Add("@mail", NpgsqlDbType.Varchar).Value = contact.Mail;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Contact ajouté.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }

        public bool Supprimer(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "CALL supprimer_contact(@id);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
                return false;
            }
        }

        public void Modifier(int id, Contact contact)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL modifier_contact(
                    @id,
                    @nom,
                    @prenom,
                    @registre,
                    @adresse,
                    @gsm,
                    @mail
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
            command.Parameters.Add("@nom", NpgsqlDbType.Varchar).Value = contact.Nom;
            command.Parameters.Add("@prenom", NpgsqlDbType.Varchar).Value = contact.Prenom;
            command.Parameters.Add("@registre", NpgsqlDbType.Varchar).Value = contact.Registre;
            command.Parameters.Add("@adresse", NpgsqlDbType.Varchar).Value = contact.Adresse;
            command.Parameters.Add("@gsm", NpgsqlDbType.Varchar).Value = contact.Gsm;
            command.Parameters.Add("@mail", NpgsqlDbType.Varchar).Value = contact.Mail;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Contact modifié.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }

        public void AfficherUn(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_un_contact(@id);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_contact"]} | " +
                    $"Nom: {reader["nom"]} | " +
                    $"Prénom: {reader["prenom"]} | " +
                    $"Registre: {reader["registre"]} | " +
                    $"Adresse: {reader["adresse"]} | " +
                    $"Gsm: {reader["gsm"]} | " +
                    $"Mail: {reader["mail"]}"
                );
            }
            else
            {
                Console.WriteLine("Contact introuvable.");
            }
        }

        public void AfficherTout()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_tous_contacts();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_contact"]} | " +
                    $"Nom: {reader["nom"]} | " +
                    $"Prénom: {reader["prenom"]} | " +
                    $"Registre: {reader["registre"]} | " +
                    $"Adresse: {reader["adresse"]} | " +
                    $"Gsm: {reader["gsm"]} | " +
                    $"Mail: {reader["mail"]}"
                );
            }
        }
        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_tous_contacts();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                contacts.Add(LireContact(reader));
            }

            return contacts;
        }

        public Contact? GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_un_contact(@id);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

            using var reader = command.ExecuteReader();

            if (reader.Read())
                return LireContact(reader);

            return null;
        }

        private Contact LireContact(NpgsqlDataReader reader)
        {
            return new Contact
            {
                IdContact = Convert.ToInt32(reader["id_contact"]),
                Nom = reader["nom"].ToString() ?? "",
                Prenom = reader["prenom"].ToString() ?? "",
                Registre = reader["registre"].ToString() ?? "",
                Adresse = reader["adresse"].ToString() ?? "",
                Gsm = reader["gsm"].ToString() ?? "",
                Mail = reader["mail"].ToString() ?? ""
            };
        }

    }
}