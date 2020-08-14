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

using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client.Impl;

namespace RabbitMQ.Client.Framing.Impl
{
    internal sealed class QueueUnbind : MethodBase
    {
        public ushort _reserved1;
        public string _queue;
        public string _exchange;
        public string _routingKey;
        public IDictionary<string, object> _arguments;

        public QueueUnbind()
        {
        }

        public QueueUnbind(ushort Reserved1, string Queue, string Exchange, string RoutingKey, IDictionary<string, object> Arguments)
        {
            _reserved1 = Reserved1;
            _queue = Queue;
            _exchange = Exchange;
            _routingKey = RoutingKey;
            _arguments = Arguments;
        }

        public override ushort ProtocolClassId => ClassConstants.Queue;
        public override ushort ProtocolMethodId => QueueMethodConstants.Unbind;
        public override string ProtocolMethodName => "queue.unbind";
        public override bool HasContent => false;

        public override void ReadArgumentsFrom(ref MethodArgumentReader reader)
        {
            _reserved1 = reader.ReadShort();
            _queue = reader.ReadShortstr();
            _exchange = reader.ReadShortstr();
            _routingKey = reader.ReadShortstr();
            _arguments = reader.ReadTable();
        }

        public override void WriteArgumentsTo(ref MethodArgumentWriter writer)
        {
            writer.WriteShort(_reserved1);
            writer.WriteShortstr(_queue);
            writer.WriteShortstr(_exchange);
            writer.WriteShortstr(_routingKey);
            writer.WriteTable(_arguments);
        }

        public override int GetRequiredBufferSize()
        {
            int bufferSize = 2 + 1 + 1 + 1; // bytes for _reserved1, length of _queue, length of _exchange, length of _routingKey
            bufferSize += Encoding.UTF8.GetByteCount(_queue); // _queue in bytes
            bufferSize += Encoding.UTF8.GetByteCount(_exchange); // _exchange in bytes
            bufferSize += Encoding.UTF8.GetByteCount(_routingKey); // _routingKey in bytes
            bufferSize += WireFormatting.GetTableByteCount(_arguments); // _arguments in bytes
            return bufferSize;
        }
    }
}
