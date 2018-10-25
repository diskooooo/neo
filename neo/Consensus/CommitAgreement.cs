﻿using System.IO;
using Neo.IO;
using Neo.Network.P2P.Payloads;

namespace Neo.Consensus
{
    internal class CommitAgreement : ConsensusMessage
    {
        /// <summary>
        /// Block hash of the signature
        /// </summary>
        //public UInt256 BlockHash;
        public Block FinalBlock;
        public byte[] FinalSignature;
        // TODO: send partials?

        /// <summary>
        /// Constructors
        /// </summary>
        public CommitAgreement() : base(ConsensusMessageType.CommitAgreement) { 
            FinalBlock = null;
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            FinalBlock = reader.ReadSerializable<Block>();
            FinalSignature = reader.ReadBytes(64);
        }

        public override void Serialize(BinaryWriter writer)
        {
            base.Serialize(writer);
            FinalBlock.Serialize(writer);
            writer.Write(FinalSignature);
        }
    }
}
