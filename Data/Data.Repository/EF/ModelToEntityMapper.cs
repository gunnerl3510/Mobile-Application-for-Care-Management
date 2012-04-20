// -----------------------------------------------------------------------
// <copyright file="ModelToEntityMapper.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Data.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;

    using Infrastructure.Model;

    using AccountModels = Infrastructure.Model.Account;
    using InsuranceModels = Infrastructure.Model.Insurance;
    using MedicalModels = Infrastructure.Model.Medical;
    using PrescriptionModels = Infrastructure.Model.Prescription;

    /// <summary>
    /// The static class used to map the business models to the EF models
    /// </summary>
    /// <typeparam name="T">
    /// The type of the business model that is being used in the repository
    /// </typeparam>
    internal static class ModelToEntityMapper<T>
        where T : IModel
    {
        /// <summary>
        /// Maps the upserts necessary for a <c>Model.IModel</c> to
        /// instance(s) of <c>EntityObject</c>s to update or insert
        /// </summary>
        public static readonly Dictionary<Type, Func<CareManagementContainer, IEnumerable<T>, IEnumerable<T>>>
            UpsertMapper = new Dictionary<Type, Func<CareManagementContainer, IEnumerable<T>, IEnumerable<T>>>
                {
                    {
                        typeof(AccountModels.Account), 
                        (context, accounts) =>
                            {
                                var accountEntities =
                                    ((IEnumerable<AccountModels.Account>)accounts).Select(
                                        account => account.ToEfAccount()).ToList();

                                accountEntities.ForEach(
                                    account => EfEntityHelpers<Account>.Upsert(context.Accounts, account));

                                return
                                    accountEntities.Select(EntityConversionExtensions.ToModelAccount) as IEnumerable<T>;
                            }
                        }, 
                    {
                        typeof(InsuranceModels.Insurer),
                        (context, insurers) =>
                            {
                                var insurerEntities =
                                    ((IEnumerable<InsuranceModels.Insurer>)insurers).Select(
                                        insurer => insurer.ToEfInsurer()).ToList();

                                insurerEntities.ForEach(
                                    insurer => EfEntityHelpers<Insurer>.Upsert(context.Insurers, insurer));

                                return
                                    insurerEntities.Select(EntityConversionExtensions.ToModelInsurer) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationRequest),
                        (context, requests) =>
                            {
                                var requestEntities =
                                    ((IEnumerable<InsuranceModels.AuthorizationRequest>)requests).Select(
                                        request => request.ToEfAuthorizationRequest()).ToList();

                                requestEntities.ForEach(
                                    request => EfEntityHelpers<AuthorizationRequest>.Upsert(context.AuthorizationRequests, request));

                                return
                                    requestEntities.Select(EntityConversionExtensions.ToModelAuthorizationRequest) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationNote),
                        (context, notes) =>
                            {
                                var noteEntities =
                                    ((IEnumerable<InsuranceModels.AuthorizationNote>)notes).Select(
                                        note => note.ToEfAuthorizationNote()).ToList();

                                noteEntities.ForEach(
                                    note => EfEntityHelpers<AuthorizationNote>.Upsert(context.AuthorizationNotes, note));

                                return
                                    noteEntities.Select(EntityConversionExtensions.ToModelAuthorizationNote) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationFollowUp),
                        (context, followUps) =>
                            {
                                var noteEntities =
                                    ((IEnumerable<InsuranceModels.AuthorizationFollowUp>)followUps).Select(
                                        followUp => followUp.ToEfAuthorizationFollowUp()).ToList();

                                noteEntities.ForEach(
                                    followUp => EfEntityHelpers<AuthorizationFollowUp>.Upsert(context.AuthorizationFollowUps, followUp));

                                return
                                    noteEntities.Select(EntityConversionExtensions.ToModelAuthorizationFollowUp) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(MedicalModels.Facility),
                        (context, facilities) =>
                            {
                                var facilityEntities =
                                    ((IEnumerable<MedicalModels.Facility>)facilities).Select(
                                        facility => facility.ToEfFacility()).ToList();

                                facilityEntities.ForEach(
                                    facility => EfEntityHelpers<Facility>.Upsert(context.Facilities, facility));

                                return
                                    facilityEntities.Select(EntityConversionExtensions.ToModelFacility) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(MedicalModels.Provider),
                        (context, providers) =>
                            {
                                var providerEntities =
                                    ((IEnumerable<MedicalModels.Provider>)providers).Select(
                                        provider => provider.ToEfProvider()).ToList();

                                providerEntities.ForEach(
                                    provider => EfEntityHelpers<Provider>.Upsert(context.Providers, provider));

                                return
                                    providerEntities.Select(EntityConversionExtensions.ToModelProvider) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(MedicalModels.MedicalAppointment),
                        (context, appointments) =>
                            {
                                var appointmentEntities =
                                    ((IEnumerable<MedicalModels.MedicalAppointment>)appointments).Select(
                                        appointment => appointment.ToEfAppointment()).ToList();

                                appointmentEntities.ForEach(
                                    appointment => EfEntityHelpers<MedicalAppointment>.Upsert(context.MedicalAppointments, appointment));

                                return
                                    appointmentEntities.Select(EntityConversionExtensions.ToModelMedicalAppointment) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(PrescriptionModels.Medication),
                        (context, medicines) =>
                            {
                                var medicineEntities =
                                    ((IEnumerable<PrescriptionModels.Medication>)medicines).Select(
                                        medicine => medicine.ToEfMedication()).ToList();

                                medicineEntities.ForEach(
                                    medicine => EfEntityHelpers<Medication>.Upsert(context.Medications, medicine));

                                return
                                    medicineEntities.Select(EntityConversionExtensions.ToModelMedication) as IEnumerable<T>;
                            }
                        },
                    {
                        typeof(PrescriptionModels.PrescriptionPickup),
                        (context, pickups) =>
                            {
                                var pickupEntities =
                                    ((IEnumerable<PrescriptionModels.PrescriptionPickup>)pickups).Select(
                                        pickup => pickup.ToEfPrescriptionPickup()).ToList();

                                pickupEntities.ForEach(
                                    pickup => EfEntityHelpers<PrescriptionPickup>.Upsert(context.PrescriptionPickups, pickup));

                                return
                                    pickupEntities.Select(EntityConversionExtensions.ToModelPrescriptionPickup) as IEnumerable<T>;
                            }
                        }
                };

        /// <summary>
        /// Maps the deletes necessary for a <seealso cref="IModel"/> to
        /// instance(s) of <seealso cref="EntityObject"/> to delete
        /// </summary>
        public static readonly Dictionary<Type, Action<CareManagementContainer, IEnumerable<T>>> DeleteMapper =
            new Dictionary<Type, Action<CareManagementContainer, IEnumerable<T>>>
                {
                    {
                        typeof(AccountModels.Account), 
                        (context, accounts) =>
                            {
                                var accountModels = accounts as IEnumerable<AccountModels.Account>;
                                if (accountModels == null)
                                {
                                    return;
                                }

                                var accountEntities = accountModels.Select(account => account.ToEfAccount()).ToList();

                                accountEntities.ForEach(
                                    account => { EfEntityHelpers<Account>.Delete(context.Accounts, account); });
                            }
                        },
                    {
                        typeof(InsuranceModels.Insurer), 
                        (context, insurers) =>
                            {
                                var insurerModels = insurers as IEnumerable<InsuranceModels.Insurer>;
                                if (insurerModels == null)
                                {
                                    return;
                                }

                                var insurerEntities = insurerModels.Select(insurer => insurer.ToEfInsurer()).ToList();

                                insurerEntities.ForEach(
                                    insurer => { EfEntityHelpers<Insurer>.Delete(context.Insurers, insurer); });
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationRequest), 
                        (context, requests) =>
                            {
                                var requestModels = requests as IEnumerable<InsuranceModels.AuthorizationRequest>;
                                if (requestModels == null)
                                {
                                    return;
                                }

                                var requestEntities = requestModels.Select(request => request.ToEfAuthorizationRequest()).ToList();

                                requestEntities.ForEach(
                                    request => { EfEntityHelpers<AuthorizationRequest>.Delete(context.AuthorizationRequests, request); });
                            }
                        },
                    {
                        typeof(InsuranceModels.AuthorizationNote), 
                        (context, notes) =>
                            {
                                var noteModels = notes as IEnumerable<InsuranceModels.AuthorizationNote>;
                                if (noteModels == null)
                                {
                                    return;
                                }

                                var noteEntities = noteModels.Select(note => note.ToEfAuthorizationNote()).ToList();

                                noteEntities.ForEach(
                                    note => { EfEntityHelpers<AuthorizationNote>.Delete(context.AuthorizationNotes, note); });
                            }
                        },
                    {
                        typeof(MedicalModels.Facility), 
                        (context, facilities) =>
                            {
                                var facilityModels = facilities as IEnumerable<MedicalModels.Facility>;
                                if (facilityModels == null)
                                {
                                    return;
                                }

                                var facilityEntities = facilityModels.Select(facility => facility.ToEfFacility()).ToList();

                                facilityEntities.ForEach(
                                    facility => { EfEntityHelpers<Facility>.Delete(context.Facilities, facility); });
                            }
                        },
                    {
                        typeof(MedicalModels.Provider), 
                        (context, providers) =>
                            {
                                var providerModels = providers as IEnumerable<MedicalModels.Provider>;
                                if (providerModels == null)
                                {
                                    return;
                                }

                                var providerEntities = providerModels.Select(provider => provider.ToEfProvider()).ToList();

                                providerEntities.ForEach(
                                    provider => { EfEntityHelpers<Provider>.Delete(context.Providers, provider); });
                            }
                        },
                    {
                        typeof(MedicalModels.MedicalAppointment), 
                        (context, appointments) =>
                            {
                                var appointmentModels = appointments as IEnumerable<MedicalModels.MedicalAppointment>;
                                if (appointmentModels == null)
                                {
                                    return;
                                }

                                var appointmentEntities = appointmentModels.Select(facility => facility.ToEfAppointment()).ToList();

                                appointmentEntities.ForEach(
                                    appointment => { EfEntityHelpers<MedicalAppointment>.Delete(context.MedicalAppointments, appointment); });
                            }
                        }
                };
    }
}
