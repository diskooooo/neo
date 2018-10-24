﻿using System.IO;
using Neo.Network.P2P.Payloads;

namespace Neo.Consensus
{
    internal class PrepareResponse : ConsensusMessage
    {
        public ConsensusPayload PreparePayload;
        public byte[] ResponseSignature; // TODO: send multiple signatures for possible speedup?
        public PrepareRequest PrepareRequestPayload() {
          byte[] seri = PreparePayload.Data;
          PrepareRequest p = new PrepareRequest();
          using (MemoryStream ms = new MemoryStream(seri, 0, seri.Length, false))
          {
              using (BinaryReader reader = new BinaryReader(ms))
              {
                  p.Deserialize(reader);
              }
          }
          return p;
        }

        public PrepareResponse()
            : base(ConsensusMessageType.PrepareResponse)
        {
        }

        public override void Deserialize(BinaryReader reader)
        {
            base.Deserialize(reader);
            //PreparePayload = reader.ReadSerializable<ConsensusPayload>();
            PreparePayload = new ConsensusPayload();
            is = PreparePayload;
            is.Deserialize(reader);
            ResponseSignature = reader.ReadBytes(16);
        }

        public override void Serialize(BinaryWriter writer)
        {
            base.Serialize(writer);
            ISerializable is = PreparePayload;
            is.Serialize(writer);
            //writer.Write(PreparePayload);
            writer.Write(ResponseSignature);
        }
    }
}
