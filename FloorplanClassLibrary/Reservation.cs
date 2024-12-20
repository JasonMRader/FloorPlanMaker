﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class Reservation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("rid")]
        public int Rid { get; set; }

        [JsonProperty("sequence_id")]
        public int SequenceId { get; set; }

        [JsonProperty("guest_id")]
        public string GuestId { get; set; } 

        [JsonProperty("guest")]
        public string Guest { get; set; } 

        [JsonProperty("confirmation_id")]
        public long? ConfirmationId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("table_number")]
        public List<string> TableNumber { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("scheduled_time")]
        public DateTime ScheduledTime { get; set; }

        [JsonProperty("party_size")]
        public int PartySize { get; set; }

        [JsonProperty("visit_tags")]
        public List<string> VisitTags { get; set; } 

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("seated_time")]
        public DateTime? SeatedTime { get; set; } 

        [JsonProperty("done_time")]
        public DateTime? DoneTime { get; set; } 

        [JsonProperty("pos_data")]
        public PosData PosData { get; set; }

        [JsonProperty("scheduled_time_utc")]
        public DateTime ScheduledTimeUtc { get; set; }

        [JsonProperty("marketing_opted_out")]
        public bool MarketingOptedOut { get; set; }

        [JsonProperty("guest_request")]
        public string GuestRequest { get; set; }

        [JsonProperty("venue_notes")]
        public object VenueNotes { get; set; } 

        [JsonProperty("opentable_notes")]
        public string OpentableNotes { get; set; }

        [JsonProperty("table_category")]
        public object TableCategory { get; set; } 

        [JsonProperty("seated_time_utc")]
        public DateTime? SeatedTimeUtc { get; set; } 

        [JsonProperty("done_time_utc")]
        public DateTime? DoneTimeUtc { get; set; } 

        [JsonProperty("created_date_utc")]
        public DateTime CreatedDateUtc { get; set; }

        [JsonProperty("updated_at_utc")]
        public DateTime UpdatedAtUtc { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("referrer")]
        public object Referrer { get; set; } 

        [JsonProperty("added_to_waitlist")]
        public object AddedToWaitlist { get; set; } 

        [JsonProperty("added_to_waitlist_utc")]
        public object AddedToWaitlistUtc { get; set; } 

        [JsonProperty("arrived_time")]
        public DateTime? ArrivedTime { get; set; } 

        [JsonProperty("arrived_time_utc")]
        public DateTime? ArrivedTimeUtc { get; set; } 

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("currency_denominator")]
        public int? CurrencyDenominator { get; set; } 
        public override string ToString()
        {
            return ScheduledTime.ToString("g") + " | " + PartySize + " | " + State;
        }
    }
}
