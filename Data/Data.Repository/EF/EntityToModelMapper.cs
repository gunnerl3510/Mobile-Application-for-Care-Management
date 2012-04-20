// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityToModelMapper.cs" company="LC LLC">
//   All rights reserved LC LLC
// </copyright>
// <summary>
//   Maps EF entities to Business Models
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Data.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Infrastructure.Model;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;
    using MedicalModels = Infrastructure.Model.Medical;
    using PrescriptionModels = Infrastructure.Model.Prescription;
    using SchedulingModels = Infrastructure.Model.Scheduling;

    /// <summary>
    /// Maps EF entities to Business Models
    /// </summary>
    /// <typeparam name="T">
    /// The <seealso cref="IModel"/> to map to
    /// </typeparam>
    internal static class EntityToModelMapper<T>
        where T : IModel, new()
    {
        /// <summary>
        /// Maintains the maps of an EF entity to its conversion method
        /// </summary>
        public static readonly Dictionary<Type, Func<CareManagementContainer, IQueryable<T>>> Mapper =
            new Dictionary<Type, Func<CareManagementContainer, IQueryable<T>>>
                {
                    {
                        typeof(AccountModels.Account), 
                        objectContext =>
                            {
                                return
                                    objectContext.Accounts.Select(
                                        account =>
                                        new AccountModels.Account
                                            {
                                                CurrentVersion = account.CurrentVersion,
                                                EmailAddress = account.EmailAddress,
                                                Id = account.AccountId,
                                                Name = account.Name,
                                                UserId = (Guid)account.UserId
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.Insurer), 
                        objectContext =>
                            {
                                return
                                    objectContext.Insurers.Select(
                                        insurer =>
                                        new InsuranceModels.Insurer
                                            {
                                                AccountId = insurer.AccountId,
                                                CurrentVersion = insurer.CurrentVersion,
                                                Id = insurer.InsurerId,
                                                Name = insurer.Name
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationRequest), 
                        objectContext =>
                            {
                                return
                                    objectContext.AuthorizationRequests.Select(
                                        request =>
                                        new InsuranceModels.AuthorizationRequest
                                            {
                                                AccountId = request.AccountId,
                                                CurrentVersion = request.CurrentVersion,
                                                Description = request.Description,
                                                Id = request.AuthorizationRequestId,
                                                InsurerId = request.InsurerId
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationNote), 
                        objectContext =>
                            {
                                return
                                    objectContext.AuthorizationNotes.Select(
                                        note =>
                                        new InsuranceModels.AuthorizationNote
                                            {
                                                AuthorizationRequestId = note.AuthorizationRequestId,
                                                Created = note.Created,
                                                CurrentVersion = note.CurrentVersion,
                                                Id = note.AuthorizationNoteId,
                                                Note = note.Note
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationFollowUp), 
                        objectContext =>
                            {
                                return
                                    objectContext.AuthorizationFollowUps.Select(
                                        followup =>
                                        new InsuranceModels.AuthorizationFollowUp
                                            {
                                                AccountId = followup.AccountId,
                                                AuthorizationRequestId = followup.AuthorizationRequestId,
                                                AppointmentDateTimeUtc = followup.AppointmentDateTime,
                                                CurrentVersion = followup.CurrentVersion,
                                                Description = followup.Description,
                                                Id = followup.AuthorizationFollowUpId
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(MedicalModels.Facility), 
                        objectContext =>
                            {
                                return
                                    objectContext.Facilities.Select(
                                        facility =>
                                        new MedicalModels.Facility
                                            {
                                                AccountId = facility.AccountId,
                                                CurrentVersion = facility.CurrentVersion,
                                                Id = facility.FacilityId,
                                                Name = facility.Name
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(MedicalModels.Provider), 
                        objectContext =>
                            {
                                return
                                    objectContext.Providers.Select(
                                        provider =>
                                        new MedicalModels.Provider
                                            {
                                                CurrentVersion = provider.CurrentVersion,
                                                FacilityId = provider.FacilityId,
                                                Id = provider.ProviderId,
                                                Name = provider.Name
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(MedicalModels.MedicalAppointment), 
                        objectContext =>
                            {
                                return
                                    objectContext.MedicalAppointments.Select(
                                        appointment =>
                                        new MedicalModels.MedicalAppointment
                                            {
                                                AccountId = appointment.AccountId,
                                                AppointmentDateTimeUtc = appointment.AppointmentDateTime,
                                                AppointmentLengthUnits =
                                                    (SchedulingModels.ScheduleUnits)
                                                    (appointment.ScheduleUnitId.HasValue
                                                         ? appointment.ScheduleUnitId.Value
                                                         : 0),
                                                CurrentVersion = appointment.CurrentVersion,
                                                Description = appointment.Description,
                                                Id = appointment.ProviderId,
                                                Length = appointment.Length,
                                                ProviderId = appointment.ProviderId
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(PrescriptionModels.Medication), 
                        objectContext =>
                            {
                                return
                                    objectContext.Medications.Select(
                                        medicine =>
                                        new PrescriptionModels.Medication
                                            {
                                                AccountId = medicine.AccountId,
                                                CurrentVersion = medicine.CurrentVersion,
                                                DosageUnits =
                                                    (PrescriptionModels.DosageUnits)(medicine.DosageUnitId.HasValue ? medicine.DosageUnitId.Value : 0),
                                                Id = medicine.MedicationId,
                                                Name = medicine.Name,
                                                Quantity = medicine.Quantity
                                            }) as IQueryable<T>;
                            }
                        },
                    {
                        typeof(PrescriptionModels.PrescriptionPickup), 
                        objectContext =>
                            {
                                return
                                    objectContext.PrescriptionPickups.Select(
                                        pickup =>
                                        new PrescriptionModels.PrescriptionPickup()
                                            {
                                                AccountId = pickup.AccountId,
                                                AppointmentDateTimeUtc = pickup.AppointmentDateTime,
                                                CurrentVersion = pickup.CurrentVersion,
                                                Id = pickup.PrescriptionPickupId,
                                                MedicationId = pickup.MedicationId
                                            }) as IQueryable<T>;
                            }
                        }
                };
    }
}
