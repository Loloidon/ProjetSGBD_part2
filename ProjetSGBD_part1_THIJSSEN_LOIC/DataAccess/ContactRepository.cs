using Npgsql;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            INSERT INTO contact
            (nom, prenom, registre, adresse, gsm, mail)
            VALUES
            (@nom, @prenom, @registre, @adresse, @gsm, @mail);
        ";

            using var command = new NpgsqlCommand(sql, connection);

            
            command.Parameters.AddWithValue("@nom", contact.Nom);
            command.Parameters.AddWithValue("@prenom",contact.Prenom);
            command.Parameters.AddWithValue("@registre",contact.Registre);
            command.Parameters.AddWithValue("@adresse",contact.Adresse);
            command.Parameters.AddWithValue("@gsm", contact.Gsm);
            command.Parameters.AddWithValue("@mail", contact.Mail);
                

            command.ExecuteNonQuery();
        }
        public void Supprimer(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                        DELETE FROM contact
                        WHERE id_contact = @id;
                    ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                int nbLignes = command.ExecuteNonQuery();

                if (nbLignes > 0)
                {
                    Console.WriteLine("Contact supprimé.");
                }
                else
                {
                    Console.WriteLine("Contact introuvable.");
                }
            }
            catch (PostgresException)
            {
                Console.WriteLine(
                    "Impossible de supprimer ce contact : " +
                    "il est lié à une adoption, un accueil famille " +
                    "ou une entrée/sortie du refuge."
                );
            }
        }
        public void Modifier(int id, Contact contact)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                        UPDATE contact
                        SET nom = @nom,
                            prenom = @prenom,
                            registre = @registre,
                            adresse = @adresse,
                            gsm = @gsm,
                            mail = @mail
                        WHERE id_contact = @id;
                    ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@nom", contact.Nom);
            command.Parameters.AddWithValue("@prenom", contact.Prenom);
            command.Parameters.AddWithValue("@registre", contact.Registre);
            command.Parameters.AddWithValue("@adresse", contact.Adresse);
            command.Parameters.AddWithValue("@gsm", contact.Gsm);
            command.Parameters.AddWithValue("@mail", contact.Mail);

            command.ExecuteNonQuery();
        }
        public void AfficherUn(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string sql = @"
                SELECT *
                FROM contact
                WHERE id_contact = @id;
            ";
            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            connection.Open();


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
                    $"Mail: {reader["mail"]}");


            }
            else
            {
                Console.WriteLine("Contact introuvable");
            }
            connection.Close();
        }
        public void AfficherTout()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM contact";

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
    }
}
