using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1_News.Models.DB.BestellungRep {
    public interface IRepositoryBestellung {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task<List<Bestellung>> GetBestellungbyBNrAsync(int bNr, int uId);
        Task<List<Bestellung>> GetBestellungbyUIdAsync(int uId);
        Task<List<Bestellung>> GetAllBestellungAsync();
        Task<bool> InsertAsync(Bestellung bestellung);

    }
}
