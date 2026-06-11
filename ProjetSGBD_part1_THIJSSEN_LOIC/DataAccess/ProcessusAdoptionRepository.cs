using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class ProcessusAdoptionRepository
    {
        private readonly string _connectionString;

        public ProcessusAdoptionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ajouter(ProcessusAdoption adoption)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                INSERT INTO processus_adoption
                (date_demande, statutAdoption, id_animal, id_contact)
                VALUES
                (@date_demande, @statutAdoption, @id_animal, @id_contact);
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@date_demande", adoption.DateDemande);
            command.Parameters.AddWithValue("@statutAdoption", adoption.StatutAdoption);
            command.Parameters.AddWithValue("@id_animal", adoption.IdAnimal);
            command.Parameters.AddWithValue("@id_contact", adoption.IdContact);

            command.ExecuteNonQuery();
        }

        public void Lister()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                SELECT pa.id_processus,
                       pa.date_demande,
                       pa.statutAdoption,
                       a.nom AS nom_animal,
                       c.nom AS nom_contact,
                       c.prenom AS prenom_contact
                FROM processus_adoption pa
                JOIN animal a ON pa.id_animal = a.id_animal
                JOIN contact c ON pa.id_contact = c.id_contact
                ORDER BY pa.id_processus;
            ";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID adoption: {reader["id_processus"]} | " +
                    $"Animal: {reader["nom_animal"]} | " +
                    $"Contact: {reader["nom_contact"]} {reader["prenom_contact"]} | " +
                    $"Date: {reader["date_demande"]} | " +
                    $"Statut: {reader["statutadoption"]}"
                );
            }
        }

        public void ModifierStatut(int idProcessus, string nouveauStatut)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                UPDATE processus_adoption
                SET statutAdoption = @statut
                WHERE id_processus = @id_processus;
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@statut", nouveauStatut);
            command.Parameters.AddWithValue("@id_processus", idProcessus);

            int nbLignes = command.ExecuteNonQuery();

            if (nbLignes > 0)
                Console.WriteLine("Statut modifié.");
            else
                Console.WriteLine("Adoption introuvable.");
        }
    }
}
