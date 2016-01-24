<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CSCAssignment2Service" generation="1" functional="0" release="0" Id="1b5936af-8897-47a8-80f0-a7fa10efa79b" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CSCAssignment2ServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WebRole1:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/LB:WebRole1:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WebRole1:BlobConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/MapWebRole1:BlobConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/MapWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WebRole1Instances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/MapWebRole1Instances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WebRole1:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWebRole1:BlobConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1/BlobConnectionString" />
          </setting>
        </map>
        <map name="MapWebRole1:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWebRole1Instances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1Instances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WebRole1" generation="1" functional="0" release="0" software="C:\Users\R Y Yap\Desktop\CSCService\CSCAssignment2Service\CSCAssignment2Service\csx\Debug\roles\WebRole1" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="BlobConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WebRole1&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WebRole1&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1Instances" />
            <sCSPolicyUpdateDomainMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1UpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1FaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WebRole1UpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WebRole1FaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WebRole1Instances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="326211e8-e5d6-4c1b-8bb5-c578bb63c1cf" ref="Microsoft.RedDog.Contract\ServiceContract\CSCAssignment2ServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="043b36cb-32d2-4802-9e30-e1920a4bbd61" ref="Microsoft.RedDog.Contract\Interface\WebRole1:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CSCAssignment2Service/CSCAssignment2ServiceGroup/WebRole1:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>