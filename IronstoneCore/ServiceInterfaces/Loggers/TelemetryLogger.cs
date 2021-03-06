﻿using System;
using System.Reflection;
using Microsoft.ApplicationInsights;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Microsoft.ApplicationInsights.DataContracts;
using Exception = System.Exception;

namespace Jpp.Ironstone.Core.ServiceInterfaces.Loggers
{
    public class TelemetryLogger : BaseLogger
    {
        private const string INSTRUMENTATION_KEY = "59426689-6bc0-4f42-a449-58eec75d8ba3";
        private TelemetryClient _client;
       
        public TelemetryLogger()
        {
            _client = new TelemetryClient { InstrumentationKey = INSTRUMENTATION_KEY };

            _client.Context.Session.Id = Guid.NewGuid().ToString();
            _client.Context.User.Id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            _client.Context.Device.OperatingSystem = Environment.OSVersion.VersionString;

            var acVersion = Application.Version.ToString();
            var coreVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
            _client.Context.GlobalProperties.Add("AcVersion", acVersion);
            _client.Context.GlobalProperties.Add("CoreVersion", coreVersion);

            //Possibly include modules?
            //var modules = loadedModules.GetModules().Select(a => $"{a.Name}, {a.Version}").Aggregate((current, next) => $"{current}; {next}");
            //_client.Context.GlobalProperties.Add("Modules", modules);
        }

        public TelemetryClient Client
        {
            get => _client;
            set => _client = value ?? throw new ArgumentNullException(nameof(value));
        }

        private static SeverityLevel MapSeverity(Severity sev)
        {
            var i = (int) sev;

            if (i >= 0 & i < 100) return SeverityLevel.Verbose;           
            if (i >= 100 & i < 200) return SeverityLevel.Information;
            if (i >= 200 & i < 300) return SeverityLevel.Warning;
            if (i >= 300 & i < 999) return SeverityLevel.Error;

            return SeverityLevel.Critical;
        }

        public override void Entry(string message, Severity sev)
        {
            _client.TrackTrace(message, MapSeverity(sev));
        }

        public override void LogEvent(Event eventType, string eventParameters)
        {
            _client.TrackEvent($"{eventType}:{eventParameters}");
        }

        public override void LogException(Exception exception)
        {
            _client.TrackException(exception);
        }
    }
}
