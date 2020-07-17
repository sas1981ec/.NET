using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class UserDB
    {
        readonly SQLiteAsyncConnection database;

        public UserDB(string dbpath)
        {
            database = new SQLiteAsyncConnection(dbpath);
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUsers()
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<User> GetUserById(int id)
        {
            return database.Table<User>().Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> GetUserByEmail(string email)
        {
            return database.Table<User>().CountAsync(s => s.Correo == email);
        }

        public Task<User> Login(string correo, string clave)
        {
            return database.Table<User>().Where(s => s.Correo == correo && s.Contraseña == clave).FirstOrDefaultAsync();
        }

        public Task<int> SaveUser(User stud)
        {
            return database.InsertAsync(stud);
        }
    }
}
