package com.amazon.spapi.sequencing.client.impl;

import lombok.AccessLevel;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.experimental.FieldDefaults;

@AllArgsConstructor
@FieldDefaults(makeFinal = true, level = AccessLevel.PRIVATE)
@Getter
public enum AccessMechanism {
    UPLOAD("upload"), DOWNLOAD("download");

    private String description;
}
