// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2020 VMware, Inc.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       https://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//---------------------------------------------------------------------------
//
// The MPL v2.0:
//
//---------------------------------------------------------------------------
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
//
//  Copyright (c) 2007-2020 VMware, Inc.  All rights reserved.
//---------------------------------------------------------------------------

using System.Text;

namespace RabbitMQ.Client.Framing.Impl
{
    internal sealed class ConnectionOpen : Client.Impl.MethodBase
    {
        public string _virtualHost;
        public string _reserved1;
        public bool _reserved2;

        public ConnectionOpen()
        {
        }

        public ConnectionOpen(string VirtualHost, string Reserved1, bool Reserved2)
        {
            _virtualHost = VirtualHost;
            _reserved1 = Reserved1;
            _reserved2 = Reserved2;
        }

        public override ushort ProtocolClassId => ClassConstants.Connection;
        public override ushort ProtocolMethodId => ConnectionMethodConstants.Open;
        public override string ProtocolMethodName => "connection.open";
        public override bool HasContent => false;

        public override void ReadArgumentsFrom(ref Client.Impl.MethodArgumentReader reader)
        {
            _virtualHost = reader.ReadShortstr();
            _reserved1 = reader.ReadShortstr();
            _reserved2 = reader.ReadBit();
        }

        public override void WriteArgumentsTo(ref Client.Impl.MethodArgumentWriter writer)
        {
            writer.WriteShortstr(_virtualHost);
            writer.WriteShortstr(_reserved1);
            writer.WriteBit(_reserved2);
            writer.EndBits();
        }

        public override int GetRequiredBufferSize()
        {
            int bufferSize = 1 + 1 + 1; // bytes for length of _virtualHost, length of _reserved1, bit fields
            bufferSize += Encoding.UTF8.GetByteCount(_virtualHost); // _virtualHost in bytes
            bufferSize += Encoding.UTF8.GetByteCount(_reserved1); // _reserved1 in bytes
            return bufferSize;
        }
    }
}
