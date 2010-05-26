DLL = Spica.Data.Groonga.dll
EXE = sdg.exe
OPT = -r:System.Xml.Linq.dll -r:System.ServiceModel.Web.dll -r:System.Runtime.Serialization.dll

all:
	gmcs -unsafe -target:library -out:$(DLL) $(OPT) *.cs

test:
	gmcs -unsafe -out:$(EXE) $(OPT) *.cs
