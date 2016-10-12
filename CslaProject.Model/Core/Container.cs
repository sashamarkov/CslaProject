using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;


namespace CslaProject.Model.Core
{
    public static class Container
    {
        private static readonly object SyncRoot = new object(  );

        private static volatile IKernel _kernel;

        public static IKernel Kernel {
            get {
                if ( _kernel == null ) {
                    lock ( SyncRoot ) {
                        if ( _kernel == null ) {
                            ConfigureKernel( );
                        }
                    }
                }
                return _kernel;
            }
        }

        public static void InjectInto( object target ) {
            Kernel.Inject( target );
        }

        private static void ConfigureKernel( ) {
            var kernel = new StandardKernel( new NinjectSettings {InjectNonPublic = true} );
            
            var dependencies = GetConfiguredDependencies( );
            if ( dependencies.Any( ) ) {
                foreach ( var values in dependencies.Select( d => ConfigurationManager.AppSettings[ d ].Split( ';' ) ) ) {
                    var catalogName = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, values[ 0 ] );
                    IEnumerable<string> files;
                    if ( values.Count( ) > 1 ) {
                        var searchPattern = values[ 1 ];
                        files = Directory.GetFiles( catalogName, searchPattern );
                    } else {
                        files = Directory.GetFiles( catalogName );
                    }
                    foreach ( var file in files ) {
                        var dependency = Assembly.LoadFile( Path.Combine( catalogName, file ) );
                        kernel.Load( dependency );
                    }
                }
            } else {
                kernel.Load( "*.dll" );
            }
            _kernel = kernel;
        }

        private static IEnumerable<string> GetConfiguredDependencies( ) {
            return ConfigurationManager.AppSettings.AllKeys.Where( p => p.StartsWith( "CslaProject.Depencies", true, CultureInfo.InvariantCulture ) );
        }

        public static void InjectKernel( IKernel kernel ) {
            lock ( SyncRoot ) {
                _kernel = kernel;
            }
        }
    }
}
