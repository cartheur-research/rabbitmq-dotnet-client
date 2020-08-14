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
    internal sealed class ConnectionClose : Client.Impl.MethodBase
    {
        public ushort _replyCode;
        public string _replyText;
        public ushort _classId;
        public ushort _methodId;

        public ConnectionClose()
        {
        }

        public ConnectionClose(ushort ReplyCode, string ReplyText, ushort ClassId, ushort MethodId)
        {
            _replyCode = ReplyCode;
            _replyText = ReplyText;
            _classId = ClassId;
            _methodId = MethodId;
        }

        public override ushort ProtocolClassId => ClassConstants.Connection;
        public override ushort ProtocolMethodId => ConnectionMethodConstants.Close;
        public override string ProtocolMethodName => "connection.close";
        public override bool HasContent => false;

        public override void ReadArgumentsFrom(ref Client.Impl.MethodArgumentReader reader)
        {
            _replyCode = reader.ReadShort();
            _replyText = reader.ReadShortstr();
            _classId = reader.ReadShort();
            _methodId = reader.ReadShort();
        }

        public override void WriteArgumentsTo(ref Client.Impl.MethodArgumentWriter writer)
        {
            writer.WriteShort(_replyCode);
            writer.WriteShortstr(_replyText);
            writer.WriteShort(_classId);
            writer.WriteShort(_methodId);
        }

        public override int GetRequiredBufferSize()
        {
            int bufferSize = 2 + 1 + 2 + 2; // bytes for _replyCode, length of _replyText, _classId, _methodId
            bufferSize += Encoding.UTF8.GetByteCount(_replyText); // _replyText in bytes
            return bufferSize;
        }
    }
}
