# Stage 1: Builder
FROM rust:1.70-bullseye as builder

# Install build dependencies
RUN apt-get update && apt-get install -y \
    curl \
    git \
    build-essential \
    pkg-config \
    libssl-dev \
    libclang-dev \
    cmake \
    unzip \
 && rm -rf /var/lib/apt/lists/*

# Manually download and install the Solana CLI (v1.10.32)
RUN curl -sSfL https://release.solana.com/v1.10.32/solana-release-x86_64-unknown-linux-gnu.tar.bz2 -o solana.tar.bz2 && \
    tar xjf solana.tar.bz2 && \
    mv solana-release/* /usr/local/bin/ && \
    rm -rf solana.tar.bz2 solana-release

# Switch to the nightly toolchain (required by Anchor CLI)
RUN rustup default nightly

# Install Anchor CLI from GitHub (locked version) using the nightly toolchain
RUN cargo +nightly install --git https://github.com/project-serum/anchor anchor-cli --locked

# Set the working directory and copy project files
WORKDIR /app
COPY Cargo.toml .
COPY src/ src/

# Build the release binary of your compiler service
RUN cargo build --release

# Stage 2: Final Runtime Image
FROM debian:bullseye-slim

# Install runtime dependencies
RUN apt-get update && apt-get install -y \
    libssl1.1 \
    ca-certificates \
 && rm -rf /var/lib/apt/lists/*

# Copy the compiled binary and Anchor CLI from the builder stage
COPY --from=builder /app/target/release/anchor_compiler_service /usr/local/bin/anchor_compiler_service
COPY --from=builder /usr/local/cargo/bin/anchor /usr/local/bin/anchor

# Ensure binaries are in PATH
ENV PATH="/usr/local/bin:${PATH}"
# Skip AVM check in Anchor CLI
ENV ANCHOR_CLI_SKIP_AVM=1

# Expose the port the API listens on
EXPOSE 5000

# Set the container entrypoint to run your service
ENTRYPOINT ["anchor_compiler_service"]