# RSA Key Creator

The results of a little spike into how to create RSA keys in .NET.

Usage: KeyCreator [options]

Options:
  --help            Display help.
  -f --filename     Specify output filename. Defaults to 'rsa_key'.
  -s --size         Specify key size in bits. Defaults to '2048'.

NIST guidelines recommend that keys should be at least 2048 bits. Fun fact, .NET won't let you use a key smaller than 2048 bits for signing. Figuring that out is an hour of my life I won't get back.
