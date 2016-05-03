using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Odonit.Models;

namespace Odonit.DAL
{
    public class UnitOfWork
    {
        private OdonitContext context = new OdonitContext();
        private OdonitRepository<Paciente> _pacienteRepository;
        private OdonitRepository<Persona> _personaRepository;
        private OdonitRepository<Contacto> _contactoRepository;
        private OdonitRepository<ObraSocial> _obraSocialRepository;

        public OdonitRepository<Paciente> PacienteRepository
        {
            get
            {

                if (this._pacienteRepository == null)
                {
                    this._pacienteRepository = new OdonitRepository<Paciente>(context);
                }
                return _pacienteRepository;
            }
        }

        public OdonitRepository<Persona> PersonaRepository
        {
            get
            {

                if (this._personaRepository == null)
                {
                    this._personaRepository = new OdonitRepository<Persona>(context);
                }
                return _personaRepository;
            }
        }

        public OdonitRepository<Contacto> ContactoRepository
        {
            get
            {

                if (this._contactoRepository == null)
                {
                    this._contactoRepository = new OdonitRepository<Contacto>(context);
                }
                return _contactoRepository;
            }
        }

        public OdonitRepository<ObraSocial> ObraSocialRepository
        {
            get
            {

                if (this._obraSocialRepository == null)
                {
                    this._obraSocialRepository = new OdonitRepository<ObraSocial>(context);
                }
                return _obraSocialRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}