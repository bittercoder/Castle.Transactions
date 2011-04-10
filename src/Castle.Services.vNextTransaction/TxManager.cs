﻿#region license

// Copyright 2009-2011 Henrik Feldt - http://logibit.se/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Diagnostics.Contracts;
using System.Transactions;

namespace Castle.Services.vNextTransaction
{
	internal class TxManager : ITxManager, ITxManagerInternal
	{
		Maybe<ITransaction> ITxManager.CurrentTransaction
		{
			get { throw new NotImplementedException(); }
		}

		void ITxManager.AddRetryPolicy(string policyKey, Func<Exception, bool> retryPolicy)
		{
			throw new NotImplementedException();
		}

		void ITxManager.AddRetryPolicy(string policyKey, IRetryPolicy retryPolicy)
		{
			throw new NotImplementedException();
		}

		public Maybe<ITransaction> CreateTransaction(ITransactionOption transactionOption)
		{
			if (transactionOption.TransactionMode == TransactionScopeOption.Suppress)
				return Maybe.None<ITransaction>();

			var inner = new CommittableTransaction(new TransactionOptions
			                                       	{
			                                       		IsolationLevel = transactionOption.IsolationLevel,
			                                       		Timeout = TimeSpan.MaxValue
			                                       	});


			ITransaction tx = new Transaction(inner);
			Contract.Assert(tx.State == TransactionState.Constructed);
			var maybe = Maybe.Some(tx);
			return maybe;
		}

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}
	}
}