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

    public struct TrackingRaid
    {
		public string sessionId;
        public string profileId;
        public string location;
        public string detectedMods;
        public DateTime time;
        public long timeInRaid;
        public string exitName;
        public string type;
        public ExitStatus exitStatus;
    }

    public struct TrackingPlayer : ISendableData
    { 
		[JsonIgnore] public string Action => "PLAYER";

		public string sessionId;
        public string profileId;
        public int level;
        public EPlayerSide team;
        public string name;
        public string type;
        public int group;
        public long spawnTime;
        public string mod_SAIN_brain;
        public string mod_SAIN_difficulty;

		public void PrepareForSend() { }
	}

    public struct TrackingRaidKill : ISendableData
    {
		[JsonIgnore] public string Action => "KILL";

		public long time;
        public string sessionId;
        public string profileId;
        public string killedId;
        public string weapon;
        public float distance;
        public string bodyPart;
        public string type;
        public string positionKiller;
        public string positionKilled;

		[JsonIgnore] public Vector3 positionKillerVec;
		[JsonIgnore] public Vector3 positionKilledVec;

		public void PrepareForSend() {
			positionKiller = JsonConvert.SerializeObject(positionKillerVec);
			positionKilled = JsonConvert.SerializeObject(positionKilledVec);
		}
	}

    public struct TrackingLootItem : ISendableData
    {
		[JsonIgnore] public string Action => "LOOT";

		public string sessionId;
        public string profileId;
        public long time;
        public string itemId;
        public string itemName;
        public int qty;
        public string type;
		public bool added;

		public void PrepareForSend() { }
	}

    public struct TrackingPlayerData : ISendableData
    {
		[JsonIgnore] public string Action => "POSITION";

		public string sessionId;
        public string profileId;
        public long time;
        public float x;
        public float y;
        public float z;
        public float dir;
        public float health;
        public float maxHealth;

		public void PrepareForSend() { }
	}

    public struct TrackingPlayerDeadOrUnspawned : ISendableData
    {
		[JsonIgnore] public string Action => "PLAYER_STATUS";
		
		public string sessionId;
        public string profileId;
        public long time;
		public PlayerStatus status;

		public void PrepareForSend() { }
	}

    public enum PlayerStatus {
        Alive,
        Dead,
        Unspawned,
        Unknown
    }

    public struct TrackingBallistic : ISendableData {
		[JsonIgnore] public string Action => "BALLISTIC";

		public string sessionId;
        public string profileId;
        public long time;
        public string weaponId;
        public string ammoId;
        public string hitPlayerId;
		public string source;
		public string target;

		[JsonIgnore] public Vector3 sourceVec;
		[JsonIgnore] public Vector3 targetVec;

		public void PrepareForSend() {
			source = JsonConvert.SerializeObject(sourceVec);
			target = JsonConvert.SerializeObject(targetVec);
		}
	}
}