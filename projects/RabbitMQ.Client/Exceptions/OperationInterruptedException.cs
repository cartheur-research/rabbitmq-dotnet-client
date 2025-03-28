// This source code is dual-licensed under the Apache License, version
// 2.0, and the Mozilla Public License, version 2.0.
//
// The APL v2.0:
//
//---------------------------------------------------------------------------
//   Copyright (c) 2007-2025 Broadcom. All Rights Reserved.
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
//  Copyright (c) 2007-2025 Broadcom. All Rights Reserved.
//---------------------------------------------------------------------------

using System;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Client.Exceptions
{
    /// <summary>
    /// Thrown when a session is destroyed during an RPC call to a
    /// broker. For example, if a TCP connection dropping causes the
    /// destruction of a session in the middle of a QueueDeclare
    /// operation, an OperationInterruptedException will be thrown to
    /// the caller of IChannel.QueueDeclare.
    /// </summary>
    [Serializable]
    public class OperationInterruptedException
        : RabbitMQClientException
    {
        ///<summary>
        ///Construct an OperationInterruptedException
        ///</summary>
        public OperationInterruptedException() : base("The AMQP operation was interrupted")
        {

        }
        ///<summary>
        ///Construct an OperationInterruptedException with
        ///the passed-in explanation, if any.
        ///</summary>
        public OperationInterruptedException(ShutdownEventArgs reason)
            : base($"The AMQP operation was interrupted: {reason}", reason.Exception)
        {
            ShutdownReason = reason;
        }


        ///<summary>Construct an OperationInterruptedException with
        ///the passed-in explanation and prefix, if any.</summary>
        public OperationInterruptedException(ShutdownEventArgs reason, string prefix)
            : base($"{prefix}: The AMQP operation was interrupted: {reason}", reason.Exception)
        {
            ShutdownReason = reason;
        }

        ///<summary>Retrieves the explanation for the shutdown. May
        ///return null if no explanation is available.</summary>
        public ShutdownEventArgs? ShutdownReason { get; protected set; }
    }
}
