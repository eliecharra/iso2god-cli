namespace Chilano.Xbox360.Xbe
{
    using Chilano.Xbox360.IO;
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
                    this.DigitalSignature = br.ReadBytes(0x100);
                    this.BaseAddress = br.ReadUInt32();
                    this.SizeOfHeaders = br.ReadUInt32();
                    this.SizeOfImage = br.ReadUInt32();
                    this.SizeOfImageHeader = br.ReadUInt32();
                    this.TimeDate = br.ReadBytes(4);
                    this.CertificateAddress = br.ReadUInt32();
                    this.NumberOfSections = br.ReadUInt32();
                    this.SectionHeadersAddress = br.ReadUInt32();
                    this.InitialisationFlags = (XbeInitFlags) br.ReadUInt32();
                    this.EntryPoint = br.ReadUInt32();
                    this.TLSAddress = br.ReadUInt32();
                    this.PEStackCommit = br.ReadUInt32();
                    this.PEHeapReserve = br.ReadUInt32();
                    this.PEHeapCommit = br.ReadUInt32();
                    this.PEBaseAddress = br.ReadUInt32();
                    this.PESizeOfImage = br.ReadUInt32();
                    this.PEChecksum = br.ReadUInt32();
                    this.PETimeDate = br.ReadBytes(4);
                    this.DebugPathnameAddress = br.ReadUInt32();
                    this.DebugFilenameAddress = br.ReadUInt32();
                    this.DebugUnicodeFilenameAddress = br.ReadUInt32();
                    this.KernelImageThunkAddress = br.ReadUInt32();
                    this.NonKernelImportDirectoryAddress = br.ReadUInt32();
                    this.NumberOfLibraryVersions = br.ReadUInt32();
                    this.LibraryVersionsAddress = br.ReadUInt32();
                    this.KernelLibraryVersionAddress = br.ReadUInt32();
                    this.XAPILibraryVersionAddress = br.ReadUInt32();
                    this.LogoBitmapAddress = br.ReadUInt32();
                    this.LogoBitmapSize = br.ReadUInt32();
                    this.IsValid = true;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

