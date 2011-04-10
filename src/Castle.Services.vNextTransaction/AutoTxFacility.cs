#region license

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

using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using log4net;

namespace Castle.Services.vNextTransaction
{
	public class AutoTxFacility : AbstractFacility
	{
		private static readonly ILog _Logger = LogManager.GetLogger(typeof (AutoTxFacility));

		protected override void Init()
		{
			Kernel.Register(
				Component.For<TxInterceptor>()
					.Named("transaction.interceptor")
					.LifeStyle.Transient,
				Component.For<ITxMetaInfoStore>()
					.ImplementedBy<TxClassMetaInfoStore>()
					.Named("transaction.metaInfoStore")
					.LifeStyle.Singleton,
				Component.For<ITxManager>()
					.ImplementedBy<TxManager>()
					.Named("transaction.manager")
					.LifeStyle.Singleton
				);

			Kernel.ComponentModelBuilder.AddContributor(new TxComponentInspector());

			_Logger.Debug("initialized auto tx facility");
		}
	}
}