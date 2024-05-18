﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using productivity_hub_api.Models;

#nullable disable

namespace productivity_hub_api.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("productivity_hub_api.Models.Configuracion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("DobleFactor")
                        .HasColumnType("bit");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<bool>("NotificacionCorreo")
                        .HasColumnType("bit");

                    b.Property<string>("Tema")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("Configuraciones");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EstadoInvitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EstadoInvitaciones");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Etiqueta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTipoEvento")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoEvento");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EventoPersona", b =>
                {
                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.Property<int>("IdEstadoInvitacion")
                        .HasColumnType("int");

                    b.Property<int?>("TareaId")
                        .HasColumnType("int");

                    b.HasKey("IdEvento", "IdPersona");

                    b.HasIndex("IdEstadoInvitacion");

                    b.HasIndex("IdPersona");

                    b.HasIndex("TareaId");

                    b.ToTable("EventoPersonas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EventoRecordatorio", b =>
                {
                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdRecordatorio")
                        .HasColumnType("int");

                    b.Property<int>("IdFrecuencia")
                        .HasColumnType("int");

                    b.HasKey("IdEvento", "IdRecordatorio");

                    b.HasIndex("IdFrecuencia");

                    b.HasIndex("IdRecordatorio");

                    b.ToTable("EventoRecordatorios");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EventoTarea", b =>
                {
                    b.Property<int>("IdEvento")
                        .HasColumnType("int");

                    b.Property<int>("IdTarea")
                        .HasColumnType("int");

                    b.Property<int>("IdPrioridad")
                        .HasColumnType("int");

                    b.HasKey("IdEvento", "IdTarea");

                    b.HasIndex("IdPrioridad");

                    b.HasIndex("IdTarea");

                    b.ToTable("EventosTareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Frecuencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Frecuencias");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Notificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdTipoNotificacion")
                        .HasColumnType("int");

                    b.Property<bool>("Leida")
                        .HasColumnType("bit");

                    b.Property<string>("Mensaje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoNotificacion");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Notificaciones");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Prioridad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prioridades");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("productivity_hub_api.Models.ProyectoPersona", b =>
                {
                    b.Property<int>("IdProyecto")
                        .HasColumnType("int");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.Property<int>("IdEstadoInvitacion")
                        .HasColumnType("int");

                    b.HasKey("IdProyecto", "IdPersona");

                    b.HasIndex("IdEstadoInvitacion");

                    b.HasIndex("IdPersona");

                    b.ToTable("ProyectoPersonas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.ProyectoTarea", b =>
                {
                    b.Property<int>("IdProyecto")
                        .HasColumnType("int");

                    b.Property<int>("IdTarea")
                        .HasColumnType("int");

                    b.Property<int>("IdPrioridad")
                        .HasColumnType("int");

                    b.HasKey("IdProyecto", "IdTarea");

                    b.HasIndex("IdPrioridad");

                    b.HasIndex("IdTarea");

                    b.ToTable("ProyectoTareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Recordatorio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaHoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recordatorios");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Subtarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("IdTarea")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdTarea");

                    b.ToTable("Subtareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("FechaLimite")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPersona");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.TareaEtiqueta", b =>
                {
                    b.Property<int>("IdTarea")
                        .HasColumnType("int");

                    b.Property<int>("IdEtiqueta")
                        .HasColumnType("int");

                    b.HasKey("IdTarea", "IdEtiqueta");

                    b.HasIndex("IdEtiqueta");

                    b.ToTable("TareaEtiquetas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.TipoEvento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Icono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoEventos");
                });

            modelBuilder.Entity("productivity_hub_api.Models.TipoNotificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoNotificaciones");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Configuracion", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Usuario", "Usuario")
                        .WithOne("Configuracion")
                        .HasForeignKey("productivity_hub_api.Models.Configuracion", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Evento", b =>
                {
                    b.HasOne("productivity_hub_api.Models.TipoEvento", "TipoEvento")
                        .WithMany("Eventos")
                        .HasForeignKey("IdTipoEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoEvento");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EventoPersona", b =>
                {
                    b.HasOne("productivity_hub_api.Models.EstadoInvitacion", "EstadoInvitacion")
                        .WithMany()
                        .HasForeignKey("IdEstadoInvitacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Evento", "Evento")
                        .WithMany("EventoPersonas")
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Persona", "Persona")
                        .WithMany()
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Tarea", null)
                        .WithMany("EventoPersonas")
                        .HasForeignKey("TareaId");

                    b.Navigation("EstadoInvitacion");

                    b.Navigation("Evento");

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EventoRecordatorio", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Evento", "Evento")
                        .WithMany("EventoRecordatorios")
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Frecuencia", "Frecuencia")
                        .WithMany("EventoRecordatorios")
                        .HasForeignKey("IdFrecuencia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Recordatorio", "Recordatorio")
                        .WithMany("EventoRecordatorios")
                        .HasForeignKey("IdRecordatorio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Frecuencia");

                    b.Navigation("Recordatorio");
                });

            modelBuilder.Entity("productivity_hub_api.Models.EventoTarea", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Evento", "Evento")
                        .WithMany("EventoTareas")
                        .HasForeignKey("IdEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Prioridad", "Prioridad")
                        .WithMany("EventoTareas")
                        .HasForeignKey("IdPrioridad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Tarea", "Tarea")
                        .WithMany("EventoTareas")
                        .HasForeignKey("IdTarea")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Prioridad");

                    b.Navigation("Tarea");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Notificacion", b =>
                {
                    b.HasOne("productivity_hub_api.Models.TipoNotificacion", "TipoNotificacion")
                        .WithMany("Notificaciones")
                        .HasForeignKey("IdTipoNotificacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Usuario", null)
                        .WithMany("Notificaciones")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("TipoNotificacion");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Persona", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Usuario", "Usuario")
                        .WithOne("Persona")
                        .HasForeignKey("productivity_hub_api.Models.Persona", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("productivity_hub_api.Models.ProyectoPersona", b =>
                {
                    b.HasOne("productivity_hub_api.Models.EstadoInvitacion", "EstadoInvitacion")
                        .WithMany()
                        .HasForeignKey("IdEstadoInvitacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Persona", "Persona")
                        .WithMany("ProyectoPersonas")
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Proyecto", "Proyecto")
                        .WithMany("ProyectoPersonas")
                        .HasForeignKey("IdProyecto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EstadoInvitacion");

                    b.Navigation("Persona");

                    b.Navigation("Proyecto");
                });

            modelBuilder.Entity("productivity_hub_api.Models.ProyectoTarea", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Prioridad", "Prioridad")
                        .WithMany("ProyectoTareas")
                        .HasForeignKey("IdPrioridad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Proyecto", "Proyecto")
                        .WithMany("ProyectoTareas")
                        .HasForeignKey("IdProyecto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Tarea", "Tarea")
                        .WithMany("ProyectoTareas")
                        .HasForeignKey("IdTarea")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prioridad");

                    b.Navigation("Proyecto");

                    b.Navigation("Tarea");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Subtarea", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Tarea", "Tarea")
                        .WithMany("Subtareas")
                        .HasForeignKey("IdTarea")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarea");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Tarea", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Persona", "Persona")
                        .WithMany("Tareas")
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Persona");
                });

            modelBuilder.Entity("productivity_hub_api.Models.TareaEtiqueta", b =>
                {
                    b.HasOne("productivity_hub_api.Models.Etiqueta", "Etiqueta")
                        .WithMany("TareaEtiquetas")
                        .HasForeignKey("IdEtiqueta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("productivity_hub_api.Models.Tarea", "Tarea")
                        .WithMany("TareaEtiquetas")
                        .HasForeignKey("IdTarea")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etiqueta");

                    b.Navigation("Tarea");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Etiqueta", b =>
                {
                    b.Navigation("TareaEtiquetas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Evento", b =>
                {
                    b.Navigation("EventoPersonas");

                    b.Navigation("EventoRecordatorios");

                    b.Navigation("EventoTareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Frecuencia", b =>
                {
                    b.Navigation("EventoRecordatorios");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Persona", b =>
                {
                    b.Navigation("ProyectoPersonas");

                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Prioridad", b =>
                {
                    b.Navigation("EventoTareas");

                    b.Navigation("ProyectoTareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Proyecto", b =>
                {
                    b.Navigation("ProyectoPersonas");

                    b.Navigation("ProyectoTareas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Recordatorio", b =>
                {
                    b.Navigation("EventoRecordatorios");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Tarea", b =>
                {
                    b.Navigation("EventoPersonas");

                    b.Navigation("EventoTareas");

                    b.Navigation("ProyectoTareas");

                    b.Navigation("Subtareas");

                    b.Navigation("TareaEtiquetas");
                });

            modelBuilder.Entity("productivity_hub_api.Models.TipoEvento", b =>
                {
                    b.Navigation("Eventos");
                });

            modelBuilder.Entity("productivity_hub_api.Models.TipoNotificacion", b =>
                {
                    b.Navigation("Notificaciones");
                });

            modelBuilder.Entity("productivity_hub_api.Models.Usuario", b =>
                {
                    b.Navigation("Configuracion")
                        .IsRequired();

                    b.Navigation("Notificaciones");

                    b.Navigation("Persona")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
