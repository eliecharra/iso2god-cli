namespace Chilano.Xbox360.Xbe
{
    using IO;
    using System;

    public class XbeHeader
    {
        public uint BaseAddress;
        public uint CertificateAddress;
        public uint DebugFilenameAddress;
        public uint DebugPathnameAddress;
        public uint DebugUnicodeFilenameAddress;
        public byte[] DigitalSignature = new byte[0x100];
        public uint EntryPoint;
        public XbeInitFlags InitialisationFlags;
        public bool IsValid;
        public uint KernelImageThunkAddress;
        public uint KernelLibraryVersionAddress;
        public uint LibraryVersionsAddress;
        public uint LogoBitmapAddress;
        public uint LogoBitmapSize;
        public uint NonKernelImportDirectoryAddress;
        public uint NumberOfLibraryVersions;
        public uint NumberOfSections;
        public uint PEBaseAddress;
        public uint PEChecksum;
        public uint PEHeapCommit;
        public uint PEHeapReserve;
        public uint PESizeOfImage;
        public uint PEStackCommit;
        public byte[] PETimeDate = new byte[4];
        public uint SectionHeadersAddress;
        public uint SizeOfHeaders;
        public uint SizeOfImage;
        public uint SizeOfImageHeader;
        public byte[] TimeDate = new byte[4];
        public uint TLSAddress;
        public uint XAPILibraryVersionAddress;

        public XbeHeader(CBinaryReader br)
        {
            try
            {
                if (br.ReadUInt32() == 0x48454258)
                {
                    DigitalSignature = br.ReadBytes(0x100);
                    BaseAddress = br.ReadUInt32();
                    SizeOfHeaders = br.ReadUInt32();
                    SizeOfImage = br.ReadUInt32();
                    SizeOfImageHeader = br.ReadUInt32();
                    TimeDate = br.ReadBytes(4);
                    CertificateAddress = br.ReadUInt32();
                    NumberOfSections = br.ReadUInt32();
                    SectionHeadersAddress = br.ReadUInt32();
                    InitialisationFlags = (XbeInitFlags) br.ReadUInt32();
                    EntryPoint = br.ReadUInt32();
                    TLSAddress = br.ReadUInt32();
                    PEStackCommit = br.ReadUInt32();
                    PEHeapReserve = br.ReadUInt32();
                    PEHeapCommit = br.ReadUInt32();
                    PEBaseAddress = br.ReadUInt32();
                    PESizeOfImage = br.ReadUInt32();
                    PEChecksum = br.ReadUInt32();
                    PETimeDate = br.ReadBytes(4);
                    DebugPathnameAddress = br.ReadUInt32();
                    DebugFilenameAddress = br.ReadUInt32();
                    DebugUnicodeFilenameAddress = br.ReadUInt32();
                    KernelImageThunkAddress = br.ReadUInt32();
                    NonKernelImportDirectoryAddress = br.ReadUInt32();
                    NumberOfLibraryVersions = br.ReadUInt32();
                    LibraryVersionsAddress = br.ReadUInt32();
                    KernelLibraryVersionAddress = br.ReadUInt32();
                    XAPILibraryVersionAddress = br.ReadUInt32();
                    LogoBitmapAddress = br.ReadUInt32();
                    LogoBitmapSize = br.ReadUInt32();
                    IsValid = true;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

