using EFT;
using BepInEx;
using UnityEngine;
using Comfort.Common;
using System.Collections.Generic;
using System;
using EFT.HealthSystem;
using Newtonsoft.Json;

namespace RAID_REVIEW
{

	public interface ISendableData {
		string Action { get; }

		void PrepareForSend();
	}

    public class TrackingRaid
    {
        public string sessionId { get; set; }
        public string profileId { get; set; }
        public string location { get; set; }
        public string detectedMods { get; set; }
        public DateTime time { get; set; }
        public long timeInRaid { get; set; }
        public string exitName { get; set; }
        public string type { get; set; }
        public ExitStatus exitStatus { get; set; }
    }

    public class TrackingPlayer : ISendableData
    { 
		[JsonIgnore] public string Action => "PLAYER";

        public string sessionId { get; set; }
        public string profileId { get; set; }
        public int level { get; set; }
        public EPlayerSide team { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int group {  get; set; }
        public long spawnTime { get; set; }
        public string mod_SAIN_brain { get; set; }
        public string mod_SAIN_difficulty { get; set; }

		public void PrepareForSend() { }
	}

    public class TrackingRaidKill : ISendableData
    {
		[JsonIgnore] public string Action => "KILL";

		public long time { get; set; }
        public string sessionId { get; set; }
        public string profileId { get; set; }
        public string killedId { get; set; }
        public string weapon {  get; set; }
        public float distance { get; set; }
        public string bodyPart {  get; set; }
        public string type { get; set; }
        public string positionKiller { get; set; }
        public string positionKilled { get; set; }

		[JsonIgnore] public Vector3 positionKillerVec { get; set; }
		[JsonIgnore] public Vector3 positionKilledVec { get; set; }

		public void PrepareForSend() {
			positionKiller = JsonConvert.SerializeObject(positionKillerVec);
			positionKilled = JsonConvert.SerializeObject(positionKilledVec);
		}
	}

    public class TrackingLootItem : ISendableData
    {
		[JsonIgnore] public string Action => "LOOT";

		public string sessionId { get; set; }
        public string profileId { get; set; }
        public long time { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public int qty { get; set; }
        public string type { get; set; }
        public bool added {  get; set; }

		public void PrepareForSend() { }
	}

    public class TrackingPlayerData : ISendableData
    {
		[JsonIgnore] public string Action => "POSITION";

		public string sessionId { get; set; }
        public string profileId { get; set; }
        public long time { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float dir { get; set; }
        public float health { get; set; }
        public float maxHealth { get; set; }

		public void PrepareForSend() { }
	}

    public class TrackingPlayerDeadOrUnspawned : ISendableData
    {
		[JsonIgnore] public string Action => "PLAYER_STATUS";
		
		public string sessionId { get; set; }
        public string profileId { get; set; }
        public long time { get; set; }
        public PlayerStatus status { get; set; }

		public void PrepareForSend() { }
	}

    public enum PlayerStatus {
        Alive,
        Dead,
        Unspawned,
        Unknown
    }

    public class TrackingBallistic : ISendableData {
		[JsonIgnore] public string Action => "BALLISTIC";

		public string sessionId { get; set; }
        public string profileId { get; set; }
        public long time { get; set; }
        public string weaponId { get; set; }
        public string ammoId { get; set; }
        public string hitPlayerId { get; set; }
		public string source { get; set; }
		public string target { get; set; }

        [JsonIgnore] public Vector3 sourceVec { get; set; }
		[JsonIgnore] public Vector3 targetVec { get; set; }

		public void PrepareForSend() {
			source = JsonConvert.SerializeObject(sourceVec);
			target = JsonConvert.SerializeObject(targetVec);
		}
	}
}